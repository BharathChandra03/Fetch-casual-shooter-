using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public enum ButtonType
    {
        Left,
        Right,
        Shoot
    }
    public ButtonType buttonType;

    public PlayerContoller playerController;

    public void OnPointerDown(PointerEventData eventData)
    { 
        if(buttonType == ButtonType.Left)
        {
            playerController.moveLeft = true;
        }
        else if(buttonType == ButtonType.Right)
        {
            playerController.moveRight = true;
        }
        else if (buttonType == ButtonType.Shoot)
        {
            playerController.FoodProjectile();
            playerController.shooting = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (buttonType == ButtonType.Left)
            playerController.moveLeft = false;
        else if (buttonType == ButtonType.Right)
            playerController.moveRight = false;
        else
            playerController.shooting = false;
    }


    /* public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.name == "LeftButton")
        {
            playerController.moveLeft = true;
            playerController.moveRight = false;
        }
        else if (gameObject.name == "RightButton")
        {
            playerController.moveRight = true;
            playerController.moveLeft = false;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        playerController.moveLeft = false;
        playerController.moveRight = false;
    }*/

    
}
