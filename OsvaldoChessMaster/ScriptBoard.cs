using System;
using System.Collections.Generic;
using System.Text;

namespace OsvaldoChessMaster
{
    public class ScriptBoard //: MonoBehaviour
    {
        static Board board1 = new Board(true);
        //public Text text1;
        bool player1 = true;
        //private InputPlayer inputJugador;
        //private Transform selectorTransform;
        private int x1;
        private int y1;
        private int x2;
        private int y2;
        private bool flag1 = false; //true si el target o selector toca una pieza
        private bool flag2 = false; // true para actualzar piezas en unity
        private bool originRead = false;
        //public PieceBase[,] myChessBoard;
        //public GameObject checkBanner;
        //public GameObject cantMoveCheckBanner;
        //public GameObject checkMateBanner;
        //public GameObject pawnWhite;
        //public GameObject pawnBlack;
        //public GameObject rookWhite;
        //public GameObject rookBlack;
        //public GameObject nighWhite;
        //public GameObject nighBlack;
        //public GameObject bishWhite;
        //public GameObject bishBlack;
        //public GameObject kingWhite;
        //public GameObject kingBlack;
        //public GameObject queeWhite;
        //public GameObject queeBlack;
        static public bool flag3;
        static public bool flag4;
        static public bool flag5;

        public ArtificialIntelligence artificInt = new ArtificialIntelligence(board1);


        //void Start()
        //{
        //    selectorTransform = GetComponent<Transform>();
        //    inputJugador = GetComponent<InputPlayer>();
        //    printPieces(board1.ChessBoard);
        //}

        //void Update()
        //{

        //    // -----------------------carteles ---------------------------
        //    if (board1.IsCheckFlag && !flag3) //flag3 es para instanciar solo una vez
        //    {
        //        GameObject checkText = Instantiate(checkBanner, new Vector3(8.8f, 2.3f, 6.2f), Quaternion.Euler(90f, 0f, 0f));
        //        //checkText.transform.position = new Vector3(9f, 2.3f, 8f);
        //        checkText.transform.localScale = new Vector3(0.5f, 0.8f, 1f);
        //        flag3 = true;
        //    }
        //    if (board1.IsCantMoveCheckFlag && !flag4) //flag4 es para instanciar solo una vez
        //    {
        //        GameObject checkText2 = Instantiate(cantMoveCheckBanner, new Vector3(8.8f, 2.3f, 4.9f), Quaternion.Euler(90f, 0f, 0f));
        //        checkText2.transform.localScale = new Vector3(0.5f, 0.8f, 1f);
        //        flag4 = true;
        //    }
        //    if (board1.IsCheckmateFlag && !flag5) //flag5 es para instanciar solo una vez
        //    {
        //        GameObject checkText3 = Instantiate(checkMateBanner, new Vector3(9.1f, 2.3f, 5.65f), Quaternion.Euler(90f, 0f, 0f));
        //        checkText3.transform.localScale = new Vector3(0.5f, 0.8f, 1f);
        //        flag5 = true;
        //    }

        //    //----------------------- Pieces regeneration -------------------
        //    if (flag2)
        //    {
        //        //Debug.Log(Board.ChessBoard[2, 8]);
        //        clearPieces();
        //        printPieces(board1.ChessBoard);
        //        flag2 = false;
        //    }

        //    //---------------------------- A.I.-------------------
        //    //if (!board1.Turn)
        //    //{
        //    //    try
        //    //    {
        //    //        //Debug.Log("la repsuesta es: " + artificInt.BestComputerMoveDepth4(board1).x1 + artificInt.BestComputerMoveDepth4(board1).y1 + artificInt.BestComputerMoveDepth4(board1).x2 + artificInt.BestComputerMoveDepth4(board1).y2 + player1);
        //    //        if (board1.FinallyMove(artificInt.BestComputerMoveDepth4(board1).x1, artificInt.BestComputerMoveDepth4(board1).y1, artificInt.BestComputerMoveDepth4(board1).x2, artificInt.BestComputerMoveDepth4(board1).y2))
        //    //        {
        //    //            flag2 = true; // para que se imprima el cambio
        //    //        }
        //    //    }
        //    //    catch
        //    //    {
        //    //        Debug.Log("Exception, Esperando el algoritmo");
        //    //    }

        //    //}

        //    //------------------------ mouse -------------------------------------------
        //    if (Input.GetMouseButtonDown(0) && !originRead)
        //    {
        //        flag1 = false;
        //        originRead = true;
        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        Physics.Raycast(ray, out RaycastHit hit);
        //        Vector3 newPosition = hit.collider.transform.position;
        //        if (hit.collider.name == "Board" || hit.collider.name == "Plane")
        //        {
        //            flag1 = true;
        //            originRead = false;
        //        }
        //        else
        //        {
        //            x1 = (int)Math.Round(newPosition[0], 0);
        //            y1 = (int)Math.Round(newPosition[2], 0);
        //            this.transform.position = new Vector3(x1, 1.45f, y1); //moves the selector
        //            this.GetComponent<Renderer>().material.color = Color.red;
        //        }
        //        //Debug.Log("x1 = " + x1 + "y1 = " + y1);

        //    }
        //    else if (Input.GetMouseButtonDown(0) && originRead)
        //    {
        //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        Physics.Raycast(ray, out RaycastHit hit);
        //        Vector3 hitPosition = hit.point;
        //        x2 = (int)Math.Round(hitPosition[0], 0);
        //        y2 = (int)Math.Round(hitPosition[2], 0);
        //        this.transform.position = new Vector3(x2, 1.45f, y2); //moves the selector
        //        this.GetComponent<Renderer>().material.color = Color.white;
        //        //Debug.Log("x2 = " + x2 + "y2 = " + y2);

        //        if (board1.FinallyMove(x1, y1, x2, y2))
        //        {
        //            Vector3 newPosition = new Vector3(x2, 2, y2);
        //            Vector3 originPosition = new Vector3(x1, 2, y1);
        //            if (!IsEmpty(newPosition))
        //            {
        //                Destroy(getCollider(newPosition).gameObject);
        //            }
        //            getCollider(originPosition).transform.position = newPosition; // mueve pieza                               
        //            originRead = false;
        //            flag2 = true;
        //        }
        //        else
        //        {
        //            originRead = false;
        //        }

        //    }

        //    //--------------------- keyboard   ----------------------------------

        //    if (inputJugador.pushReturn && flag1 && !originRead)
        //    {
        //        flag1 = false;
        //        originRead = true;
        //        x1 = (int)Math.Round(selectorTransform.position[0], 0);
        //        y1 = (int)Math.Round(selectorTransform.position[2], 0);
        //        this.GetComponent<Renderer>().material.color = Color.red;

        //        //Vector3 selectorScreenPosition = Camera.main.WorldToScreenPoint(selectorTransform.position) - new Vector3(0, 0, -10f);            
        //        //HacerAlgoConPiezaAhi(selectorScreenPosition);
        //    }
        //    else if (inputJugador.pushReturn && originRead)
        //    {
        //        x2 = (int)Math.Round(selectorTransform.position[0], 0);
        //        y2 = (int)Math.Round(selectorTransform.position[2], 0);
        //        if (board1.FinallyMove(x1, y1, x2, y2))
        //        {
        //            Vector3 newPosition = new Vector3(x2, 2, y2);
        //            Vector3 originPosition = new Vector3(x1, 2, y1);
        //            if (!IsEmpty(newPosition))
        //            {
        //                Destroy(getCollider(newPosition).gameObject);
        //            }
        //            getCollider(originPosition).transform.position = newPosition; // mueve pieza
        //            this.GetComponent<Renderer>().material.color = Color.white;
        //            //if (!IsEmpty(originPosition))
        //            //{
        //            //    Destroy(getCollider(originPosition).gameObject);
        //            //}
        //            originRead = false;
        //            flag2 = true;
        //        }
        //        else
        //        {
        //            originRead = false;
        //        }

        //    }
        //}



        //void InstantiateOnPosition(Vector3 mousePos)
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(mousePos);

        //    //Esto vuelve coordenadas del mouse a coordenadas 3D
        //    //mousePos.z = 10.0f; //distance of the plane from the camera
        //    //Debug.Log("coordenada" + Camera.main.ScreenToWorldPoint(mousePos));

        //    if (Physics.Raycast(ray, out RaycastHit hit))
        //    {
        //        //Debug.Log(hit.collider.name);
        //        if (hit.collider.name != "Board" && hit.collider.name != "Plane")
        //        {
        //            Transform objectHit = hit.transform;
        //            //Vector3 newPosition = new Vector3(4, 2, 4);
        //            //objectHit.position = newPosition;
        //            Vector3 newPosition = objectHit.position;
        //            x2 = (int)Math.Round(newPosition[0], 0);
        //            y2 = (int)Math.Round(newPosition[2], 0);
        //            board1.FinallyMove(x1, y1, x2, y2);
        //        }
        //    }
        //}


        //private void OnTriggerEnter(Collider other)
        //{
        //    flag1 = true;
        //}

        //public Collider getCollider(Vector3 position3D)
        //{
        //    Vector3 selectorScreenPosition = Camera.main.WorldToScreenPoint(position3D) - new Vector3(0, 0, -10f);
        //    Ray ray = Camera.main.ScreenPointToRay(selectorScreenPosition);
        //    if (Physics.Raycast(ray, out RaycastHit hit))
        //    {
        //        return hit.collider;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        //private bool IsEmpty(Vector3 position3D)
        //{
        //    Vector3 selectorScreenPosition = Camera.main.WorldToScreenPoint(position3D) - new Vector3(0, 0, -10f);
        //    Ray ray = Camera.main.ScreenPointToRay(selectorScreenPosition);
        //    Physics.Raycast(ray, out RaycastHit hit);
        //    //Debug.Log("impacto con: " + hit.collider.name);
        //    if (hit.collider.name == "Board" || hit.collider.name == "Plane" || hit.collider.name == "target")
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public PieceBase GetPiece(int x, int y, PieceBase[,] myChessBoard)
        //{
        //    return myChessBoard[x, y] ?? null;
        //}



        //private void printPieces(PieceBase[,] myChessBoard)
        //{
        //    PieceBase piece1;
        //    for (int i = Constants.ForStart; i < Constants.Size; i++)
        //    {
        //        for (int j = Constants.ForStart; j < Constants.Size; j++)
        //        {
        //            Vector3 intantiationPoint = new Vector3(i, 2, j);
        //            piece1 = GetPiece(i, j, myChessBoard);

        //            if (piece1 == null)
        //            {
        //                //Vector3 destroyPosition = new Vector3(i, 2, j);
        //                //Debug.Log("getCollider(destroyPosition): " + getCollider(destroyPosition));
        //                //Destroy(getCollider(destroyPosition).gameObject);                    
        //            }

        //            else if (piece1.GetType() == typeof(Pawn))
        //            {
        //                if (piece1.Color)
        //                {
        //                    Instantiate(pawnWhite, intantiationPoint, Quaternion.Euler(90f, 0f, 0f));
        //                }
        //                else
        //                {
        //                    Instantiate(pawnBlack, intantiationPoint, Quaternion.Euler(90f, 0f, 0f));
        //                }
        //            }
        //            else if (piece1.GetType() == typeof(Rook))
        //            {
        //                if (piece1.Color)
        //                {
        //                    Instantiate(rookWhite, intantiationPoint, Quaternion.Euler(90f, 0f, 0f));
        //                }
        //                else
        //                {
        //                    Instantiate(rookBlack, intantiationPoint, Quaternion.Euler(90f, 0f, 0f));
        //                }
        //            }
        //            else if (piece1.GetType() == typeof(Knight))
        //            {
        //                if (piece1.Color)
        //                {
        //                    Instantiate(nighWhite, intantiationPoint, Quaternion.Euler(90f, 0f, 0f));
        //                }
        //                else
        //                {
        //                    Instantiate(nighBlack, intantiationPoint, Quaternion.Euler(90f, 0f, 0f));
        //                }
        //            }
        //            else if (piece1.GetType() == typeof(Bishop))
        //            {
        //                if (piece1.Color)
        //                {
        //                    Instantiate(bishWhite, intantiationPoint, Quaternion.Euler(90f, 0f, 0f));
        //                }
        //                else
        //                {
        //                    Instantiate(bishBlack, intantiationPoint, Quaternion.Euler(90f, 0f, 0f));
        //                }
        //            }
        //            else if (piece1.GetType() == typeof(King))
        //            {
        //                if (piece1.Color)
        //                {
        //                    Instantiate(kingWhite, intantiationPoint, Quaternion.Euler(90f, 0f, 0f));
        //                }
        //                else
        //                {
        //                    Instantiate(kingBlack, intantiationPoint, Quaternion.Euler(90f, 0f, 0f));
        //                }
        //            }
        //            else if (piece1.GetType() == typeof(Queen))
        //            {
        //                if (piece1.Color)
        //                {
        //                    Instantiate(queeWhite, intantiationPoint, Quaternion.Euler(90f, 0f, 0f));
        //                }
        //                else
        //                {
        //                    Instantiate(queeBlack, intantiationPoint, Quaternion.Euler(90f, 0f, 0f));
        //                }
        //            }
        //        }
        //    }
        //}

        //private void clearPieces()
        //{
        //    for (int i = Constants.ForStart; i < Constants.Size; i++)
        //    {
        //        for (int j = Constants.ForStart; j < Constants.Size; j++)
        //        {
        //            Vector3 positionToClear = new Vector3(i, 1.5f, j);
        //            if (getCollider(positionToClear).gameObject.name != "target" && getCollider(positionToClear).gameObject.name != "Plane" && getCollider(positionToClear).gameObject.name != "Board")
        //            {
        //                //Debug.Log("i j" + i + j);
        //                Destroy(getCollider(positionToClear).gameObject);
        //            }
        //        }
        //    }
        //}

        ////public void setText(string text)
        ////{
        ////    text1.text = text;
        ////}    
    }
}
