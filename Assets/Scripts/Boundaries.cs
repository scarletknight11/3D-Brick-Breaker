using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    public GameObject leftWall, rightWall, topWall, bottomWall;

    public GameObject lCorner, rCorner;

    float distanceToCamera;
    Vector3 screenBounaries;
    Vector3 screenPoint;

    void Start()
    {
        distanceToCamera = Camera.main.transform.position.z;
        CalculateBoundaries();
    }

    void CalculateBoundaries()
    {
        float frustrumHeight = 2.0f * distanceToCamera * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float frustumWidth = frustrumHeight * Camera.main.aspect;

        //Screen Boundary
        screenBounaries = new Vector3(frustumWidth, frustrumHeight, 0);
        //find screen point
        screenPoint = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
        //Left Wall
        Vector3 leftPoint = new Vector3(screenPoint.x - Mathf.Abs(frustumWidth) / 2, screenPoint.y, 0);
        leftWall.transform.position = leftPoint;
        leftWall.transform.localScale = new Vector3(1, Mathf.Abs(screenBounaries.y), 1);
        //Right Wall
        Vector3 rightPoint = new Vector3(screenPoint.x + Mathf.Abs(frustumWidth) / 2, screenPoint.y, 0);
        rightWall.transform.position = rightPoint;
        rightWall.transform.localScale = new Vector3(1, Mathf.Abs(screenBounaries.y), 1);
        //Top Wall
        Vector3 topPoint = new Vector3(screenPoint.x, screenPoint.y + Mathf.Abs(frustrumHeight)/2, 0);
        topWall.transform.position = topPoint;
        topWall.transform.localScale = new Vector3(Mathf.Abs(screenBounaries.x), 1, 1);
        //Bottom Wall
        Vector3 bottomPoint = new Vector3(screenPoint.x, screenPoint.y - Mathf.Abs(frustrumHeight) / 2, 0);
        bottomWall.transform.position = bottomPoint;
        bottomWall.transform.localScale = new Vector3(Mathf.Abs(screenBounaries.x), 1, 1);
        //R CORNER
        rCorner.transform.position = new Vector3(rightPoint.x, topPoint.y, 0);
        //L CORNER
        lCorner.transform.position = new Vector3(leftPoint.x, topPoint.y, 0);

    }
}