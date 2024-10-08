using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    public Vector3Int Position; //셀의 그리드 내 위치
    public bool IsOccupied; //셀이 건물로 차있는지 여부
    public GameObject Building; //셀에 배치된 건물 객체

    public GridCell(Vector3Int position) //생성자 객체가 호출 될때
    {
        Position = position;
        IsOccupied = false;
        Building = null;
    }
}

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] private int width = 10; //그리드 가로 크기
    [SerializeField] private int height = 10; //그리드 세로 크기
    [SerializeField] private float cellSize = 1; //각 셀의 크기

    [SerializeField] private GameObject cellPrefab; //셀 프리팹
    [SerializeField] private GameObject buildingPrefabs; //건물 프리팹
    [SerializeField] private PlayerController playerController; //플레이어 컨트롤러 참조
    [SerializeField] private float maxBuildDistance = 5f; //건설 가능한 최대 거리
    
    [SerializeField] private Grid grid;  //그리드 선언 후 받아온다.
    private GridCell[,] cells; //2차 배열로 선언 GridCell
    private Camera firstPersonCamera; //플레이어 1인칭 카메라를 가죠온다.
    //private GridCell[,]cells;

    void Staet()
    {
        grid = GetComponent<Grid>(); //시작할 때 그리드를 받아온다
    }
    private void OnDrawGizmos() //Gizmo를 표시해주는 함수
    {
        Gizmos.color = Color.blue;
        for (int x = 0; x < height; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 cellCenter = grid.GetCellCenterWorld(new Vector3Int(x, 0, z)); //그리드 중심 점을 가져온다.
                Gizmos.DrawWireCube(cellCenter, new Vector3(cellSize, 0.1f, cellSize)); //각각의 칸을 그려준다.
            }
        }
    }
    void Start()
    {
        CreateGrid();    
    }
    //그리드를 생성하고 셀을 초기화하는 함수
    private void CreateGrid()
    {
        grid.cellSize = new Vector3(cellSize, cellSize, cellSize); //설정한 셀 사이즈를 그리드 컴포넌트에 넣는다.
        cells = new GridCell[width, height]; //그리드 가로 세로 칸수 설정
        Vector3 gridCenter = playerController.transform.position; //플레이어 중심으로 그리드 생성(원하는 위치로 변경가능)
        gridCenter.y = 0;
        transform.position = gridCenter - new Vector3(width * cellSize / 2.0f, 0, height * cellSize / 2); //그리드 중심으로 이동 시킨다.

        for(int x = 0; x <width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                Vector3Int cellPosition = new Vector3Int(x, 0, z); //(0,0,0) (1,0,0),(2,0,0)~(10,0,10) for 문 돌리면서 배치
                Vector3 worldPosition = grid.GetCellCenterWorld(cellPosition); //grid를 통해 월드 포지션을 가죠온다.
                GameObject cellObject = Instantiate(cellPosition, worldPosition, cellPrefab.transform.rotation); //설장한 그리드를 만든다.
                cellObject.transform.SetParent(transform); //지금 오브젝트 하위로 설정 한다.

                cells[x, z] = new GridCell(cellPosition); //셀 데이터 값을 생성한다
            }
        }
    }
}
