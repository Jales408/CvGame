using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPathFinder : MonoBehaviour
{

    public int horizontalGridLength;//X
    public int verticalGridLength;//X

    [Space(20)]
    public Vector3 GridWorldOffset;

    private int[,] worldRepresentation;

    private bool[,] isAccessible;

    public EnnemiObjective[] objectives;

    private void Awake()
    {
        initializeWorldRepresentation();
    }

    private void Start() {
        
    }

    private void initializeWorldRepresentation(){
        worldRepresentation = new int[horizontalGridLength,verticalGridLength];
        isAccessible = new bool[horizontalGridLength,verticalGridLength];
        /*
        for( int x =0;x<horizontalGridLength;x++){
            for( int z=0;z<verticalGridLength;z++){
                if(!(z==0 || z ==verticalGridLength-1 || x==0 || x==horizontalGridLength-1)){
                    isAccessible[x,z] = true;
                }
            }
        }
        isAccessible[5,5] = false;
        isAccessible[4,4] = false;*/

        for( int x =0;x<horizontalGridLength;x++){
            for( int z=0;z<verticalGridLength;z++){
                Vector3 raycastOrigin = GridWorldOffset + new Vector3(x,5f,z);
                RaycastHit hit;
                int mask = (1<< LayerMask.NameToLayer("BlockingEnvironnement"));
                if(!Physics.Raycast(raycastOrigin,-transform.up,out hit,5,mask)){
                    isAccessible[x,z] = true;
                }
            }
        }
    }

    private void OnDrawGizmos() {
        for( int x =0;x<horizontalGridLength;x++){
            for( int z=0;z<verticalGridLength;z++){
                Vector3 raycastOrigin = GridWorldOffset + x*Vector3.right + z*Vector3.forward;
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(raycastOrigin,new Vector3(0.2f,1.0f,0.2f));
            }
        }
    }

    public List<Vector3> findPath(Vector3 startPosition, Vector3 targetPosition){
        List<Vector3> path = new List<Vector3>();
        worldRepresentation = new int[horizontalGridLength,verticalGridLength];
        
        //get gridCoordonates
        Vector3 normalizeStartPosition = startPosition - GridWorldOffset;
        Vector3 normalizeTargetPosition = targetPosition - GridWorldOffset;

        //convertPositionOngrid
        int[] gridStartPosition = new int[2]{Mathf.RoundToInt(normalizeStartPosition.x),Mathf.RoundToInt(normalizeStartPosition.z)};
        int[] gridTargetPosition = new int[2]{Mathf.RoundToInt(normalizeTargetPosition.x),Mathf.RoundToInt(normalizeTargetPosition.z)};

        //Explore map from start to find end
        int maxCaseSecurity = 0;
        int[] maxStepCase = new int[3]{0,0,0};

        List<int[]> frontiere = new List<int[]>();
        frontiere.Add(new int[3]{gridStartPosition[0],gridStartPosition[1],0});
        while(frontiere.Count!=0 && maxCaseSecurity<horizontalGridLength*verticalGridLength){
            maxCaseSecurity++;
            //Pop old frontiere
            int[] actualFrontiereCaseWeighted = frontiere[0];
            frontiere.RemoveAt(0);
            //addSurrondingCase
            foreach(int[] gridCase in getUsableCloseCase(actualFrontiereCaseWeighted)){
                frontiere.Add(gridCase);
                worldRepresentation[gridCase[0],gridCase[1]] = gridCase[2];
            };
            if(actualFrontiereCaseWeighted[0]==gridTargetPosition[0] && actualFrontiereCaseWeighted[1]==gridTargetPosition[1]){
                maxStepCase = actualFrontiereCaseWeighted;
                break;
            }
        }
        //findPathFromMap
        maxCaseSecurity = 0;
        int[] actualStepCaseWeighted = maxStepCase;
        
        while(actualStepCaseWeighted[2]>1 && maxCaseSecurity<horizontalGridLength+verticalGridLength){
            maxCaseSecurity++;
            List<int[]> potentialStepsWeighted = getCloserCases(actualStepCaseWeighted);
            int randomChoice = Random.Range(0,potentialStepsWeighted.Count);
            actualStepCaseWeighted = potentialStepsWeighted[randomChoice];
            path.Add(new Vector3(actualStepCaseWeighted[0],0,actualStepCaseWeighted[1])+GridWorldOffset+Vector3.left*Random.Range(-0.5f,0.5f)+Vector3.forward*Random.Range(-0.5f,0.5f));
        }
        return path;
    }

    private List<int[]> getUsableCloseCase(int[] gridCase){
        List<int[]> closeCases = new List<int[]>();
        int x = gridCase[0];
        int y = gridCase[1];
        int step = gridCase[2];
        if(x+1<horizontalGridLength-1 && isAccessible[x+1,y] && worldRepresentation[x+1,y]==0){
            closeCases.Add(new int[]{x+1,y,step+1});
        }
        if(x>0 && isAccessible[x-1,y] && worldRepresentation[x-1,y]==0){
            closeCases.Add(new int[]{x-1,y,step+1});
        }
        if(y+1<verticalGridLength-1 && isAccessible[x,y+1] && worldRepresentation[x,y+1]==0){
            closeCases.Add(new int[]{x,y+1,step+1});
        }
        if(y>0 && isAccessible[x,y-1] && worldRepresentation[x,y-1]==0){
            closeCases.Add(new int[]{x,y-1,step+1});
        }
        return closeCases;
    }

    private List<int[]> getCloserCases(int[] gridCase){
        List<int[]> closeCases = new List<int[]>();
        int x = gridCase[0];
        int y = gridCase[1];
        int step = gridCase[2];
        if(x+1<horizontalGridLength-1 && isAccessible[x+1,y] && worldRepresentation[x+1,y]<step){
            closeCases.Add(new int[]{x+1,y,worldRepresentation[x+1,y]});
        }
        if(x>0 && isAccessible[x-1,y] && worldRepresentation[x-1,y]<step){
            closeCases.Add(new int[]{x-1,y,worldRepresentation[x-1,y]});
        }
        if(y+1<verticalGridLength-1 && isAccessible[x,y+1] && worldRepresentation[x,y+1]<step){
            closeCases.Add(new int[]{x,y+1,worldRepresentation[x,y+1]});
        }
        if(y>0 && isAccessible[x,y-1] && worldRepresentation[x,y-1]<step){
            closeCases.Add(new int[]{x,y-1,worldRepresentation[x,y-1]});
        }
        return closeCases;
    }

    public EnnemiObjective selectRandomObjective(){
        int randombjective = Random.Range(0,objectives.Length);
        for(int i = 0;i<objectives.Length;i++){
            int index = (randombjective+i)%objectives.Length;
            if(objectives[index].GetAvaillability()){
                return objectives[index];
            }
        }
        return null;
    }
}
