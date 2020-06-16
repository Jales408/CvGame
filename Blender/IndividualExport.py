import bpy
import os


EXPORT_DIRECTORY = 'Mesh_Exports'
def isParent(parent, potentialChild):
		potentialParent = potentialChild.getParent()
		if potentialParent is not None and potentialParent.name == parent.name:
			return True
		else:
			return False

def getChildren(obj, scene): 
	return [child for child in scene.getChildren() if isParent(obj, child)]

def recursivelyGetChildren(obj,scene):
    childrenList = obj.getChildren(obj,scene)
    if len(childrenList)==0:
        return []
    else:
        additionalChildrenList = []
        for child in childrenList:
            additionalChildrenList.append(recursivelyGetChildren(child))
        return childrenList.append(additionalChildrenList)


def hasParent(obj):
    parent = obj.getParent()
    return(p is not None)

class USEFUL_CUSTOM_OT_export_operator(bpy.types.Operator):
    bl_idname = "useful_custom.export_operator"
    bl_label = "Export Operator"
    
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
            selection = [ obj for obj in bpy.context.visible_objects if obj.type == 'MESH']
        else :
            selection = [ obj for obj in bpy.context.selected_objects if obj.type == 'MESH']

        bpy.ops.object.select_all(action='DESELECT')

        
        for obj in selection :

            bpy.ops.object.mode_set(mode='OBJECT')
            name = bpy.path.clean_name(obj.name)
            
            #Export Selection
            obj.select_set(True)
            bpy.ops.export_scene.fbx(filepath = os.path.join(filepath, obj.name)+ '.fbx',use_selection=True)
            obj.select_set(False)
        
        return {'FINISHED'}
     
class CustomExportMenu(bpy.types.Menu):
    bl_label = "Useful_Export"
    bl_idname = "OBJECT_MT_custom_menu_export"

    def draw(self, context):
        layout = self.layout

        props = layout.operator("useful_custom.export_operator", text="Export Visible Meshes")
        props.select_all = True;

        props = layout.operator("useful_custom.export_operator", text="Export Selected Meshes")
        props.select_all = False;
        

def draw_item(self, context):
    layout = self.layout
    layout.menu(CustomExportMenu.bl_idname)


def register():
    
    bpy.utils.register_class(USEFUL_CUSTOM_OT_export_operator)
    bpy.utils.register_class(CustomExportMenu)

    # lets add ourselves to the main header
    bpy.types.INFO_HT_header.append(draw_item)


def unregister():
    bpy.utils.unregister_class(CustomExportMenu)

    bpy.types.INFO_HT_header.remove(draw_item)
    bpy.utils.unregister_class(USEFUL_CUSTOM_OT_export_operator)

if __name__ == "__main__":
    register()

    # The menu can also be called from scripts
    # bpy.ops.wm.call_menu(name=CustomMenu.bl_idname)
