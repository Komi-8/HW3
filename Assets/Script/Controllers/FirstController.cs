using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    LandControl leftLandController, rightLandController;
    River river;
    BoatControl boatController;
    RoleControl[] roleControllers;
    MoveCtrl moveController;
    bool isRunning;
    float time;

    public void LoadResources()
    {
        //role
        roleControllers = new RoleControl[6];
        for (int i = 0; i < 6; ++i)
        {
            roleControllers[i] = new RoleControl();
            roleControllers[i].CreateRole(Position.role_land[i], i < 3 ? true : false, i);
        }

        //land
        leftLandController = new LandControl();
        leftLandController.CreateLand(Position.left_land);
        leftLandController.GetLand().land.name = "left_land";
        rightLandController = new LandControl();
        rightLandController.CreateLand(Position.right_land);
        rightLandController.GetLand().land.name = "right_land";

        //将人物添加并定位至左岸
        foreach (RoleControl roleController in roleControllers)
        {
            roleController.GetRoleModel().role.transform.localPosition = leftLandController.AddRole(roleController.GetRoleModel());
        }
        //boat
        boatController = new BoatControl();
        boatController.CreateBoat(Position.left_boat);

        //river
        river = new River(Position.river);

        //move
        moveController = new MoveCtrl();

        isRunning = true;
        time = 60;


    }

    public void MoveBoat()
    {
        if (isRunning == false || moveController.GetIsMoving()) return;
        if (boatController.GetBoatModel().isRight)
        {
            moveController.SetMove(Position.left_boat, boatController.GetBoatModel().boat);
        }
        else
        {
            moveController.SetMove(Position.right_boat, boatController.GetBoatModel().boat);
        }
        boatController.GetBoatModel().isRight = !boatController.GetBoatModel().isRight;
    }

    public void MoveRole(Role roleModel)
    {
        if (isRunning == false || moveController.GetIsMoving()) return;
        if (roleModel.inBoat)
        {
            if (boatController.GetBoatModel().isRight)
            {
                moveController.SetMove(rightLandController.AddRole(roleModel), roleModel.role);
            }
            else
            {
                moveController.SetMove(leftLandController.AddRole(roleModel), roleModel.role);
            }
            roleModel.onRight = boatController.GetBoatModel().isRight;
            boatController.RemoveRole(roleModel);
        }
        else
        {
            if (boatController.GetBoatModel().isRight == roleModel.onRight)
            {
                if (roleModel.onRight)
                {
                    rightLandController.RemoveRole(roleModel);
                }
                else
                {
                    leftLandController.RemoveRole(roleModel);
                }
                moveController.SetMove(boatController.AddRole(roleModel), roleModel.role);
            }
        }
    }

    public void Check()
    {
        if (isRunning == false) return;
        this.gameObject.GetComponent<UserGUI>().gameMessage = "";
        if (rightLandController.GetLand().priestCount == 3)
        {
            this.gameObject.GetComponent<UserGUI>().gameMessage = "You Win!";
            isRunning = false;
        }
        else
        {
            int leftPriestCount, rightPriestCount, leftDevilCount, rightDevilCount;
            leftPriestCount = leftLandController.GetLand().priestCount + (boatController.GetBoatModel().isRight ? 0 : boatController.GetBoatModel().priestCount);
            rightPriestCount = rightLandController.GetLand().priestCount + (boatController.GetBoatModel().isRight ? boatController.GetBoatModel().priestCount : 0);
            leftDevilCount = leftLandController.GetLand().devilCount + (boatController.GetBoatModel().isRight ? 0 : boatController.GetBoatModel().devilCount);
            rightDevilCount = rightLandController.GetLand().devilCount + (boatController.GetBoatModel().isRight ? boatController.GetBoatModel().devilCount : 0);
            if (leftPriestCount != 0 && leftPriestCount < leftDevilCount || rightPriestCount != 0 && rightPriestCount < rightDevilCount)
            {
                this.gameObject.GetComponent<UserGUI>().gameMessage = "Game Over!";
                isRunning = false;
            }
        }
    }

    void Awake()
    {
        SSDirector.GetInstance().CurrentSceneController = this;
        LoadResources();
        this.gameObject.AddComponent<UserGUI>();
    }

    void Update()
    {

    }
}
