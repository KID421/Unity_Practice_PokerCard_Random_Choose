using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

[ExecuteInEditMode]
public class CardSystem : MonoBehaviour
{
    public List<GameObject> cards = new List<GameObject>();

    public string[] cardType = { "Spades", "Diamond", "Heart", "Club" };

    private void Start()
    {
        if (cards.Count == 0)
        {
            for (int i = 0; i < cardType.Length; i++)
            {
                for (int j = 1; j < 14; j++)
                {
                    string number = j.ToString();

                    switch (j)
                    {
                        case 1:
                            number = "A";
                            break;
                        case 11:
                            number = "J";
                            break;
                        case 12:
                            number = "Q";
                            break;
                        case 13:
                            number = "K";
                            break;
                    }

                    GameObject card = Resources.Load<GameObject>("PlayingCards_" + number + cardType[i]);
                    cards.Add(card);
                }
            }
        }
    }

    public void ChooseCard(string type)
    {
        DestoryCardObject();

        var cardChoose = cards.Where((x) => x.name.Contains(type));

        foreach (var item in cardChoose)
        {
            Instantiate(item, transform);
        }

        StartCoroutine(SetPosition());
    }

    public void CardSort()
    {
        DestoryCardObject();

        var cardSort = from card in cards
                       where card.name.Contains(cardType[3]) || card.name.Contains(cardType[2]) || card.name.Contains(cardType[1]) || card.name.Contains(cardType[0])
                       select card;


        foreach (var item in cardSort)
        {
            Instantiate(item, transform);
        }

        StartCoroutine(SetPosition());
    }

    public void Shuffle()
    {
        DestoryCardObject();

        List<GameObject> cardsTemp = cards.ToArray().ToList();
        List<GameObject> cardsShuffle = new List<GameObject>();

        for (int i = 0; i < 52; i++)
        {
            int r = Random.Range(0, cardsTemp.Count);
            GameObject temp = cardsTemp[r];
            cardsShuffle.Add(temp);
            cardsTemp.RemoveAt(r);

            Instantiate(cardsShuffle[i], transform);
        }

        StartCoroutine(SetPosition());
    }

    private void DestoryCardObject()
    {
        for (int i = 0; i < transform.childCount; i++) Destroy(transform.GetChild(i).gameObject);
    }

    private IEnumerator SetPosition()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < transform.childCount; i++)
        {
            float x = i % 13;
            int y = i / 13;

            Transform card = transform.GetChild(i);
            card.position = new Vector3((x - 6) * 1.3f, y * 2f - 2, 0);
            card.eulerAngles = new Vector3(180, 0, 0);
            card.localScale = Vector3.one * 20;

            yield return null;
        }
    }
}