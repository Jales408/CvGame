import bpy
import os


ROTATE_OPTION = True
x_offset = 3.141592/2

EXPORT_DIRECTORY = 'Mesh_Exports'

def isParent(parent, potentialChild):
		potentialParent = potentialChild.parent
		if potentialParent is not None and potentialParent.name == parent.name:
			return True
		else:
			return False

def getChildren(obj): 
	return [child for child in bpy.context.visible_objects if isParent(obj, child)]

def recursivelyGetChildren(obj):
    childrenList = getChildren(obj)
    if not len(childrenList)==0:
        additionalChildrenList = []
        for child in childrenList:
            liste = recursivelyGetChildren(child)
            additionalChildrenList.extend(liste)
        childrenList.extend(additionalChildrenList)
    return childrenList


def hasParent(obj):
    return(obj.parent is not None)

class USEFUL_CUSTOM_OT_export_parented_operator(bpy.types.Operator):
    bl_idname = "useful_custom.export_parented_operator"
    bl_label = "Parent Export Operator"
    
    select_all : bpy.props.BoolProperty(name="useful_export_selection_mode")
    
    @classmethod
    def poll(cls, context):
        return context.active_object is not None
    
    def execute(self, context):    

        bpy.ops.object.mode_set(mode='OBJECT')
        
        #Get filepath to stock fbx
        filepath = os.path.join( os.path.dirname(bpy.data.filepath), EXPORT_DIRECTORY)
        if not os.path.isdir(filepath):
            try:
                os.makedirs(filepath)
            except:
                msg('FAILED to create find and create Folder: path tried -> %s\n' %(filepath))
                return False
        
        #Get all meshes desired
        if self.select_all :
            selection = [ obj for obj in bpy.context.visible_objects if obj.type == 'MESH' and not hasParent(obj)]
        else :
            selection = [ obj for obj in bpy.context.selected_objects if obj.type == 'MESH' and not hasParent(obj)]

        bpy.ops.object.select_all(action='DESELECT')

        
        for obj in selection :
            if(ROTATE_OPTION):
                obj.select_set(True)
                bpy.context.view_layer.objects.active = obj
                bpy.ops.object.transform_apply( scale=True, rotation = True )
                obj.rotation_euler.x -= x_offset
                bpy.context.view_layer.objects.active = obj
                bpy.ops.object.transform_apply( rotation = True )
                obj.rotation_euler.x += x_offset
            bpy.ops.object.mode_set(mode='OBJECT')
            name = bpy.path.clean_name(obj.name)
            
            #Export Selection
            exportList = recursivelyGetChildren(obj) + [obj]

            for objToSelect in exportList:
                objToSelect.select_set(True)
            bpy.ops.export_scene.fbx(filepath = os.path.join(filepath, obj.name)+ '.fbx',use_selection=True, apply_scale_options='FBX_SCALE_UNITS')
            for objToDeselect in exportList:
                objToDeselect.select_set(False)

        for obj in selection:
            obj.select_set(True)

        return {'FINISHED'}
     
class CustomParentExportMenu(bpy.types.Menu):
    bl_label = "Useful_Parent_Export"
    bl_idname = "OBJECT_MT_custom_menu_parent_export"

    def draw(self, context):
        layout = self.layout

        props = layout.operator("useful_custom.export_parented_operator", text="Export Visible Meshes")
        props.select_all = True;

        props = layout.operator("useful_custom.export_parented_operator", text="Export Selected Meshes")
        props.select_all = False;
        

def draw_item(self, context):
    layout = self.layout
    layout.menu(CustomParentExportMenu.bl_idname)


def register():
    
    bpy.utils.register_class(USEFUL_CUSTOM_OT_export_parented_operator)
    bpy.utils.register_class(CustomParentExportMenu)

    # lets add ourselves to the main header
    bpy.types.INFO_HT_header.append(draw_item)


def unregister():
    bpy.utils.unregister_class(CustomParentExportMenu)

    bpy.types.INFO_HT_header.remove(draw_item)
    bpy.utils.unregister_class(USEFUL_CUSTOM_OT_export_parented_operator)

if __name__ == "__main__":
    register()

    # The menu can also be called from scripts
    # bpy.ops.wm.call_menu(name=CustomMenu.bl_idname)
