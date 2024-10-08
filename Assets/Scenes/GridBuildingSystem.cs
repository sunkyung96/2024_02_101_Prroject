using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    public Vector3Int Position; //���� �׸��� �� ��ġ
    public bool IsOccupied; //���� �ǹ��� ���ִ��� ����
    public GameObject Building; //���� ��ġ�� �ǹ� ��ü

    public GridCell(Vector3Int position) //������ ��ü�� ȣ�� �ɶ�
    {
        Position = position;
        IsOccupied = false;
        Building = null;
    }
}

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] private int width = 10; //�׸��� ���� ũ��
    [SerializeField] private int height = 10; //�׸��� ���� ũ��
    [SerializeField] private float cellSize = 1; //�� ���� ũ��

    [SerializeField] private GameObject cellPrefab; //�� ������
    [SerializeField] private GameObject buildingPrefabs; //�ǹ� ������
    [SerializeField] private PlayerController playerController; //�÷��̾� ��Ʈ�ѷ� ����
    [SerializeField] private float maxBuildDistance = 5f; //�Ǽ� ������ �ִ� �Ÿ�
    
    [SerializeField] private Grid grid;  //�׸��� ���� �� �޾ƿ´�.
    private GridCell[,] cells; //2�� �迭�� ���� GridCell
    private Camera firstPersonCamera; //�÷��̾� 1��Ī ī�޶� ���ҿ´�.
    //private GridCell[,]cells;

    void Staet()
    {
        grid = GetComponent<Grid>(); //������ �� �׸��带 �޾ƿ´�
    }
    private void OnDrawGizmos() //Gizmo�� ǥ�����ִ� �Լ�
    {
        Gizmos.color = Color.blue;
        for (int x = 0; x < height; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 cellCenter = grid.GetCellCenterWorld(new Vector3Int(x, 0, z)); //�׸��� �߽� ���� �����´�.
                Gizmos.DrawWireCube(cellCenter, new Vector3(cellSize, 0.1f, cellSize)); //������ ĭ�� �׷��ش�.
            }
        }
    }
    void Start()
    {
        CreateGrid();    
    }
    //�׸��带 �����ϰ� ���� �ʱ�ȭ�ϴ� �Լ�
    private void CreateGrid()
    {
        grid.cellSize = new Vector3(cellSize, cellSize, cellSize); //������ �� ����� �׸��� ������Ʈ�� �ִ´�.
        cells = new GridCell[width, height]; //�׸��� ���� ���� ĭ�� ����
        Vector3 gridCenter = playerController.transform.position; //�÷��̾� �߽����� �׸��� ����(���ϴ� ��ġ�� ���氡��)
        gridCenter.y = 0;
        transform.position = gridCenter - new Vector3(width * cellSize / 2.0f, 0, height * cellSize / 2); //�׸��� �߽����� �̵� ��Ų��.

        for(int x = 0; x <width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                Vector3Int cellPosition = new Vector3Int(x, 0, z); //(0,0,0) (1,0,0),(2,0,0)~(10,0,10) for �� �����鼭 ��ġ
                Vector3 worldPosition = grid.GetCellCenterWorld(cellPosition); //grid�� ���� ���� �������� ���ҿ´�.
                GameObject cellObject = Instantiate(cellPosition, worldPosition, cellPrefab.transform.rotation); //������ �׸��带 �����.
                cellObject.transform.SetParent(transform); //���� ������Ʈ ������ ���� �Ѵ�.

                cells[x, z] = new GridCell(cellPosition); //�� ������ ���� �����Ѵ�
            }
        }
    }
}
