using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private RectTransform m_RectTransform;
    private CanvasGroup m_CanvasGroup;
    private bool dragged = false;
    private Canvas m_Canvas;



    public GameObject referenceClickHolderPrefab;
    private GameObject clickHolderPrefab = null;


    private void Awake()
    {
        m_Canvas = GameObject.Find("Canvas0").GetComponent<Canvas>();
        m_RectTransform = GetComponent<RectTransform>();
        m_CanvasGroup = GetComponent<CanvasGroup>();
    }

    private GameObject currentSlot;
    private Sprite itemImage;
    private string itemName;
    private int power;
    private Types type;
    private int price;
    public enum Types 
    {

        Pot,
        Shield,
        Armor,
        Bow,
        Sword
    }


    public enum Items
    {
        ShieldLv1,
        ShieldLv2,
        ShieldLv3,
        ShieldLv4,
        ShieldLv5,
        ArmorLv1,
        ArmorLv2,
        ArmorLv3,
        BowLv1,
        BowLv2,
        SwordLv1,
        SwordLv2,
        SwordLv3,
        SwordLv4,
        SpearLv1,
        SpearLv2,
        SpearLv3,
        SpearLv4,
        AxeLv1,
        AxeLv2,
        CanPotu
    }


    public Sprite ItemImage { get => itemImage; set => itemImage = value; }
    public string ItemName { get => itemName; set => itemName = value; }
    public int Power { get => power; set => power = value; }
    public int Fiyat { get => price; set => price = value; }
    public Types Tur { get => type; set => type = value; }
    public GameObject CurrentSlot { get => currentSlot; set => currentSlot = value; }

    public void setAll(string itemName, int power, Types tur, Sprite itemImage,int fiyat, GameObject currentSlot)
    {
        this.itemName = itemName;
        this.power = power;
        this.type = tur;
        this.itemImage = itemImage;
        this.price = fiyat;
        this.currentSlot = currentSlot;
        GetComponent<Image>().sprite = itemImage;
    }

    public void changeSlot(GameObject slot)
    {
        m_RectTransform.position = slot.GetComponent<RectTransform>().position;
        CurrentSlot.GetComponent<Slot>().setItem(null);

        CurrentSlot = slot;
        slot.GetComponent<Slot>().setItem(this.gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!dragged)
        {
            Vector3 pos = transform.position;
            pos.x = pos.x + 200;
            pos.y = pos.y + -200;

            clickHolderPrefab = Instantiate(referenceClickHolderPrefab, pos, Quaternion.identity, transform);
            clickHolderPrefab.GetComponent<ClickHolder>().setItem(this.gameObject);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (clickHolderPrefab != null)
        {
            Destroy(clickHolderPrefab);
            clickHolderPrefab = null;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (clickHolderPrefab != null)
        {
            Destroy(clickHolderPrefab);
            clickHolderPrefab = null;
        }

        dragged = true;
        m_CanvasGroup.blocksRaycasts = false;
        m_CanvasGroup.alpha = 0.7f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_CanvasGroup.blocksRaycasts = true;
        m_CanvasGroup.alpha = 1f;
        dragged = false;

        m_RectTransform.position = currentSlot.GetComponent<RectTransform>().position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_RectTransform.anchoredPosition += eventData.delta / m_Canvas.scaleFactor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = m_RectTransform.anchoredPosition;
        }
    }


}
