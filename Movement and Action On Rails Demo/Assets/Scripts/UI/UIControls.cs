using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControls : MonoBehaviour
{
    public Image topPanel, leftPanel, rightPanel, bottomPanel;

    public void SetPanels(ScrollDirection directionsEnabled)
    {
        topPanel.gameObject.SetActive( (directionsEnabled & ScrollDirection.Top) == ScrollDirection.Top);
        leftPanel.gameObject.SetActive( (directionsEnabled & ScrollDirection.Left) == ScrollDirection.Left);
        rightPanel.gameObject.SetActive( (directionsEnabled & ScrollDirection.Right) == ScrollDirection.Right);
        bottomPanel.gameObject.SetActive( (directionsEnabled & ScrollDirection.Bottom) == ScrollDirection.Bottom);
    }

    public void SetTopPanel(bool set)
    {
        topPanel.gameObject.SetActive(set);
    }

    public void SetLeftPanel(bool set)
    {
        leftPanel.gameObject.SetActive(set);
    }

    public void SetRightPanel(bool set)
    {
        rightPanel.gameObject.SetActive(set);
    }

    public void SetBottomPanel(bool set)
    {
        bottomPanel.gameObject.SetActive(set);
    }

    public void SetExclusivePanel(ScrollDirection sd, bool set)
    {
        Debug.Log(sd.ToString());
        switch (sd)
        {
            case ScrollDirection.Top:
                topPanel.gameObject.SetActive(set);
                break;
            case ScrollDirection.Left:
                leftPanel.gameObject.SetActive(set);
                break;
            case ScrollDirection.Right:
                rightPanel.gameObject.SetActive(set);
                break;
            case ScrollDirection.Bottom:
                bottomPanel.gameObject.SetActive(set);
                break;
            default:
                break;
        }
    }
}
