﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class csStoreManager : MonoBehaviour
{

    //장비 상점 메뉴 
    public GameObject equipageMenu;

    //능력치 상점 메뉴
    public GameObject abilityScroll;
    public GameObject equInven;

    //아이템 상점 메뉴
    public GameObject itemMenu;

    //던전 입장 메뉴
    public GameObject dungeonMaun;

    //상점 오픈시 인벤토리 오브젝트
    public GameObject equipageScroll;

    //카메라의 위치 값
    Transform maincamera;

    //시작버튼
    public GameObject startButton;

    //타이틀
    public GameObject title;

    //인벤버튼 메뉴버튼
    public GameObject invenButton;
    public GameObject itemShopButton;
    public GameObject eqShopButton;
    public GameObject abilShopButton;
    public GameObject dungeonButton;
  

    public void onStartButton()
    {
        iTween.MoveTo(GameObject.Find("Main Camera"), iTween.Hash("x", -2.0f
                                            , "y", 6.0f
                                            , "z", 24.0f
                                            , "time", 5.0f));
        iTween.RotateTo(GameObject.Find("Main Camera"), iTween.Hash("x", 20                                         
                                           , "time", 5.0f));
        itemShopButton.SetActive(true);
        eqShopButton.SetActive(true);
        abilShopButton.SetActive(true);
        invenButton.SetActive(true);
        dungeonButton.SetActive(true);
        title.SetActive(false);
        startButton.SetActive(false);        
    }

    public void onEqShopButton()
    {
        iTween.MoveTo(GameObject.Find("Main Camera"), iTween.Hash("x",-5.0f
                                    , "y", 1.0f
                                    , "z", 11.0f
                                    , "time", 5.0f));
        iTween.RotateTo(GameObject.Find("Main Camera"), iTween.Hash("y", 270
                                   ,"x",0
                                   , "time", 5.0f));
        StartCoroutine("equipageStorePop");
    }

    public void onAbilShopButton()
    {
        iTween.MoveTo(GameObject.Find("Main Camera"), iTween.Hash("x", 1.0f
                            , "y", 1.0f
                            , "z", 9.0f
                            , "time", 5.0f));
        iTween.RotateTo(GameObject.Find("Main Camera"), iTween.Hash("y", 90
                                   , "x", 0
                                   , "time", 5.0f));
        StartCoroutine("abilityStorePop");
    }

    public void onItemShopButton()
    {
        iTween.MoveTo(GameObject.Find("Main Camera"), iTween.Hash("x", 2.0f
                    , "y", 1.0f
                    , "z", -3.0f
                    , "time", 5.0f));
        iTween.RotateTo(GameObject.Find("Main Camera"), iTween.Hash("y", 120
                                   , "x", 0
                                   , "time", 5.0f));
        StartCoroutine("itemStorePop");
    }

    public void onDungeonButton()
    {
        iTween.MoveTo(GameObject.Find("Main Camera"), iTween.Hash("x", -7.0f
            , "y", 2.5f
            , "z", -4.3f
            , "time", 5.0f));
        iTween.RotateTo(GameObject.Find("Main Camera"), iTween.Hash("y", 180
                                   , "x", 0
                                   , "time", 5.0f));
        StartCoroutine("dungeonPop");
    }

    public void onInvenButton()
    {
        abilityScroll.SetActive(false);
        //equInven.SetActive(true);
        itemMenu.SetActive(false);
        dungeonMaun.SetActive(false);       
        equipageMenu.SetActive(false);
        equipageScroll.SetActive(false);       
    }
    

    IEnumerator equipageStorePop()
    {
        abilityScroll.SetActive(false);
        equInven.SetActive(false);
        itemMenu.SetActive(false);
        dungeonMaun.SetActive(false);
        yield return new WaitForSeconds(2.0f);        
        equipageMenu.SetActive(true);
        equipageScroll.SetActive(true);
    }

    IEnumerator abilityStorePop()
    {
        equipageMenu.SetActive(false);
        equipageScroll.SetActive(false);
        itemMenu.SetActive(false);
        dungeonMaun.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        equInven.SetActive(true);              
        abilityScroll.SetActive(true);
    
    }
    IEnumerator itemStorePop()
    {
        abilityScroll.SetActive(false);
        equInven.SetActive(false);
        equipageScroll.SetActive(false);
        dungeonMaun.SetActive(false);
        equipageMenu.SetActive(false);
        yield return new WaitForSeconds(2.0f);            
        itemMenu.SetActive(true);
    }
    IEnumerator dungeonPop()
    {
        abilityScroll.SetActive(false);
        equInven.SetActive(false);
        equipageScroll.SetActive(false);       
        equipageMenu.SetActive(false);
        itemMenu.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        dungeonMaun.SetActive(true);
    }
}
