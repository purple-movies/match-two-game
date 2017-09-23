using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BlocksManager : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] RectTransform tilesPanel;
    [SerializeField] GameObject gameWonUi;

    private int tileCount = 12;
    private int matchedCount = 0;
    private TileReference tileReferenceA;
    private TileReference tileReferenceB;
    private List<TileNode> tiles = new List<TileNode>();
    private List<SuperButton> tileButtons = new List<SuperButton>();
    private List<SuperButton> matchedButtons = new List<SuperButton>();

    void Start()
    {
        clearTiles();

        for (var i = 0; i < tileCount * .5f; i++)
        {
            var node1 = new TileNode(i);
            var node2 = new TileNode(i);
            node1.match = node2;
            node2.match = node1;
            tiles.Add(node1);
            tiles.Add(node2);
        }

        for (var i = 0; i < tileCount; i++)
        {
            var t = tiles[i];
            var j = UnityEngine.Random.Range(i, tileCount);
            tiles[i] = tiles[j];
            tiles[j] = t;
        }

        foreach (var node in tiles)
        {
            var go = Instantiate(tilePrefab);
            var button = go.GetComponent<SuperButton>();
            go.GetComponent<TileReference>().node = node;
            go.transform.SetParent(tilesPanel, false);
            button.onButtonClick = onTileClick;
            tileButtons.Add(button);
        }
    }

    private void onTileClick(SuperButton button)
    {
        var tileReference = button.GetComponent<TileReference>();
        tileReference.showValue();
        button.enabled = false;

        if (tileReferenceA == null)
        {
            tileReferenceA = tileReference;
        }
        else
        {
            setButtonsEnabled(false);
            tileReferenceB = tileReference;

            if (tileReferenceA.node.match == tileReferenceB.node)
            {


                matchedButtons.Add(tileReferenceA.GetComponent<SuperButton>());
                matchedButtons.Add(button);

                resetButtons();
                matchedCount++;
                if (matchedCount >= tileCount * .5f) onMatchWon();
            }
            else StartCoroutine(resetTiles());
        }
    }

    private void onMatchWon()
    {
        Debug.Log("YOU WIN!");
        gameWonUi.SetActive(true);
    }

    private void setButtonsEnabled(bool enabled)
    {
        foreach (var button in tileButtons)
            button.enabled = enabled;
    }

    IEnumerator resetTiles()
    {
        yield return new WaitForSeconds(1f);

        tileReferenceA.hideValue();
        tileReferenceB.hideValue();
        resetButtons();
        yield return null;
    }

    private void resetButtons()
    {
        tileReferenceA = null;
        tileReferenceB = null;

        foreach (var button in tileButtons)
            if ( ! matchedButtons.Contains(button))
                button.enabled = true;
    }

    private void clearTiles()
    {
        foreach (RectTransform trans in tilesPanel)
        {
            Destroy(trans.gameObject);
        }
    }
}
