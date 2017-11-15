using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMaster : MonoBehaviour {

    public static int room_width = 10;
    public static int room_height = 20;

    public static Transform[,] board = new Transform[room_width, room_height];

    public Vector2 toBoard (Vector3 coords) // Set coords to nearest slot on board.
    {
        return new Vector3(Mathf.Round(coords.x),Mathf.Round(coords.y), coords.z);
    }

    public bool insideBoard (Vector3 coords) // Are the coords in the board's bounds?
    {
        return ((int)coords.x >= 0 && (int)coords.x < room_width && (int)coords.y >= 0);
    }

    public bool fullRow (int y) // Do we have a full row?
    {
        for(int x = 0; x < room_width; ++x)
        {
            if(board[x,y] == null)
            {
                return false;
            }
        }
        return true;
    }

    public void deleteMino (int y)
    {
        for(int x = 0; x < room_width; ++x)
        {
            Destroy(board[x,y].gameObject);
            board[x,y] = null;
        }
    }

    public void moveRow (int y)
    {
        for(int x = 0; x < room_width; ++x)
        {
            if(board[x,y] != null)
            {
                board[x, y - 1] = board[x, y];
                board[x, y] = null;
                board[x, y - 1].position += new Vector3(0,-1,0);
            }
        }
    }

    public void moveAllRows (int y)
    {
        for (int i = y; i < room_height; ++i)
        {
            moveRow(i);
        }
    }

    public void deleteRow()
    {
        for (int y = 0; y < room_height; ++y)
        {
            if(fullRow(y))
            {
                deleteMino(y);
                moveAllRows(y + 1);
                y -= 1;
            }
        }
    }

    public bool aboveBoard(moveTetromino tetromino)
    {
        for (int x = 0; x < room_width; ++x)
        {
            foreach(Transform mino in tetromino.transform)
            {
                Vector2 coord = toBoard(mino.position);

                if (coord.y > room_height - 1)
                {
                    return true;
                }

            }
        }

        return false;
    }

    public void gameOver()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void updateBoard(moveTetromino tetromino) // Update the board.
    {
        for (int i = 0; i < room_height; ++i)
        {
            for(int j = 0; j < room_width; ++j)
            {
                if (board[j, i] != null)
                {
                    if (board[j, i].parent == tetromino.transform) //If this is the parrent of our current piece.
                    {
                        board[j, i] = null;
                    }
                }
            }
        }

        foreach(Transform mino in tetromino.transform) // Set board coord to instance of mino.
        {
            Vector2 coord = toBoard(mino.position);
            if(coord.y < room_height)
            {
                board[(int)coord.x, (int)coord.y] = mino;
            }
        }

    }

    public Transform GetTransformAtBoard(Vector2 coord) // Get instance of tetromino from coords on board.
    {
        if(coord.y > room_height - 1)
        {
            return null;
        }
        else
        {
            return board[(int)coord.x, (int)coord.y];
        }
    }
}
