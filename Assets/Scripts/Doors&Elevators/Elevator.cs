using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private float movementDistance;
    [SerializeField]
    private float movementSpeed;
    private bool goesUp;
    private bool stop = false;
    private Vector3 path_movementDistance;
    Transform upPosition;
    Transform downPosition;
    private void Awake()
    {
        upPosition = new GameObject().transform;
        downPosition = new GameObject().transform;
        path_movementDistance = new Vector3(0, movementDistance, 0);
        upPosition.position = transform.position + path_movementDistance;
        downPosition.position = transform.position;

    }

    void FixedUpdate()
    {
   
            if (goesUp && !stop)
            {
                GoUp();
            }

            else if (!goesUp && !stop)
            {
                GoDown();
            }
        }
    

        public void GoUp()
        {
            transform.position = Vector2.MoveTowards(transform.position, upPosition.position, movementSpeed * Time.deltaTime);

        }

        public void GoDown()
        {

            transform.position = Vector2.MoveTowards(transform.position, downPosition.position, (movementSpeed - 5f) * Time.deltaTime);

        }

        


        private IEnumerator WaitPlayerToGetOut()
        {
            yield return new WaitForSeconds(3f);
            stop = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {

            if (collision.gameObject.CompareTag("Player"))
            {
                //igualar la posición del jugador a la del ascensor para que no caiga constantemente
                collision.gameObject.transform.parent = transform;



                if (!stop)
                {


                    if (goesUp && Vector2.Distance(transform.position, upPosition.position) == 0)
                    {
                        stop = true;
                        StartCoroutine(WaitPlayerToGetOut());
                        goesUp = false;
                        stop = true;
                    

                }






                    else if (!goesUp && Vector2.Distance(transform.position, downPosition.position) == 0 && !stop)
                    {
                        stop = true;
                        StartCoroutine(WaitPlayerToGetOut());
                        goesUp = true;
                        movementSpeed -= 5f;
                        stop = true;
                        collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                }





                }
            }


        }


        //GETTERS Y SETTERS

        public void SetGoesUp(bool goesUp)
        {
            this.goesUp = goesUp;
        }

    }
