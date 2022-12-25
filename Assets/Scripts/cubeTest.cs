using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cubeTest : MonoBehaviour
{
    [SerializeField]private TMP_Text dataP;
    [SerializeField]private TMP_Text dataK;
    [SerializeField]private TMP_Text velocity;
    [SerializeField]private TMP_Text potencial;
    [SerializeField] private Transform VArrow;
    [SerializeField] private GameObject kArrow;
    [SerializeField] private GameObject pArrow;

    private Vector3 pref;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Sequence()
            .Append(transform.DOLocalMoveY(0.721f, 2))
            .SetEase(Ease.InOutQuart)
            .AppendInterval(2)
            .Append(transform.DOLocalMoveY(-0.567f, 2))
            .SetEase(Ease.InOutQuart)
            .AppendInterval(2)
            .SetLoops(-1);
        /*        DOTween.Sequence()
                    .Append(VArrow.DOScaleZ(2, 2))
                    .AppendInterval(1)
                    .Append(VArrow.DOScaleZ(1, 2))
                    .SetLoops(-1);*/
        pref = kArrow.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float p = Mathf.Round((transform.localPosition.y + 0.567f) / 1.288f * 100);
        float k = (100 - Mathf.Round((transform.localPosition.y + 0.567f) / 1.288f * 100));
        dataP.text = "Fp = " + p;
        dataK.text = "Fk = " + k;
    }

    void Update()
    {
        float p = Mathf.Round((transform.localPosition.y + 0.567f) / 1.288f * 100);
        float k = (100 - Mathf.Round((transform.localPosition.y + 0.567f) / 1.288f * 100));

        var pos = kArrow.transform.position;
        var dir = Vector3.Normalize(pos - pref);
        pref = pos;

        kArrow.transform.localScale = new Vector3(1, 1, dir.y);
        pArrow.transform.localScale = new Vector3(1, 1, 3* p / 100f);

        velocity.text = ""+ dir.y;
       // potencial.text = "pottencciaalee";
    }
}

/*Приходит слепой мужик на пляж с палочкой, находит себе место. Стелит полотенце, достает из сумки крем от загара, ласты, маску. Вдруг достает резиновую бабу и начинает ее надувать. Женщина сидящая неподалеку начинает возмущаться.
- Что вы творите?
- А что не так?
- Тут дети, а вы, ни стыда ни совести!
- Вы можете русским языком объяснить, что не так то?
- Вы надувную женщину на пляж притащили!
- БЛЯТЬ, Я ЧТО, ВСЮ ЗИМУ ЛОДКУ ЕБАЛ?*/