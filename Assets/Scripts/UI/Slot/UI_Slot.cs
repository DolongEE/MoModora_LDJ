using System;

public class UI_Slot : UI_Base
{
    enum Texts
    {
        DataText,
        PlayTimeText,
    }

    public int slotNum;
    public bool isCheck;
    public float playTime;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));

        return true;
    }

    public void SetInfo(bool isSaveSlot = true)
    {
        if (Init() == false)
            Init();

        if(isSaveSlot)
        {
            slotNum = int.Parse(gameObject.name.Substring(gameObject.name.Length - 1));
            isCheck = Managers.Data.SaveDict[slotNum].isCheck;

            GetText((int)Texts.DataText).text = isCheck ? $"���� {slotNum}" : "�� ����";
            GetText((int)Texts.PlayTimeText).text = isCheck ? $"�� �÷��� �ð�: {TimeSpan.FromSeconds(Managers.Data.SaveDict[slotNum].playTime).ToString(@"hh\:mm\:ss")}" : "";
            return;
        }

        GetText((int)Texts.DataText).text = gameObject.name.Contains("Setting") ? "����" : "����";
        GetText((int)Texts.PlayTimeText).text = "";
    }
}
