using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTetromino : MonoBehaviour
{

    float timer = 0;
    public float tickSpeed = 1;

    void Update()
    {
        tickSpeed = FindObjectOfType<spawnTetromino>().globalTickSpeed;
        playerInput();
    }

    void playerInput()
    {
        //Quit Game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


        //X-Axis
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!isValid())
            {
                transform.position += new Vector3(-1, 0, 0);
            }
            else
            {
                FindObjectOfType<gameMaster>().updateBoard(this);
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                transform.position += new Vector3(-1, 0, 0);
                if (!isValid())
                {
                    transform.position += new Vector3(1, 0, 0);
                }
                else
                {
                    FindObjectOfType<gameMaster>().updateBoard(this);
                }

        }

        //Y-Axis
        //Rotate
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            transform.Rotate(0, 0, 90);
            if(!isValid())
            {
                transform.Rotate(0, 0, -90);
            }
            else
            {
                FindObjectOfType<gameMaster>().updateBoard(this);
            }
        }

        //Fall
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Time.time - timer >= tickSpeed)
        {
            transform.position += new Vector3(0, -1, 0);
            if (!isValid())
            {
                transform.position += new Vector3(0, 1, 0);

                FindObjectOfType<gameMaster>().deleteRow();

                if(FindObjectOfType<gameMaster>().aboveBoard(this))
                {
                    FindObjectOfType<gameMaster>().gameOver();
                }

                //Spawn new piece.
                enabled = false;
                FindObjectOfType<spawnTetromino>().Spawn();
            }
            else
            {
                FindObjectOfType<gameMaster>().updateBoard(this);
            }

            timer = Time.time;
        }
    }

    bool isValid()
    {
        foreach(Transform child in transform)
        {
            Vector2 coord = FindObjectOfType<gameMaster>().toBoard(child.position); //Are the pieces on the board?
            if(FindObjectOfType<gameMaster>().insideBoard(coord) == false) 
            {
                return false;
            }
            if(FindObjectOfType<gameMaster>().GetTransformAtBoard(coord) != null && FindObjectOfType<gameMaster>().GetTransformAtBoard(coord).parent != transform) // Is there a mino on the board?
            {
                return false;
            }
        }
        return true;
    }
}
