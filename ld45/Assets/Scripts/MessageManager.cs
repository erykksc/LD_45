using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class MessageManager : MonoBehaviour
{
    public int MSGDisplayedNumber=0;
    [SerializeField] private Text SenderTextDisplayer;
    [SerializeField] private Text HeaderTextDisplayer;
    [SerializeField] private Text ContentTextDisplayer;

    public string SenderText = "{United Earth Dictatorate Comunication System v.8.49.6}";
    public string[] HeaderText = new string[] 
    {
        "[Sender:Inter-planetary Mining Management of the UED]       [Status:Mission File]",
        "[Sender:Vyssyni]       [Status:Not Important]",
        "[Sender:Vyssyni]       [Status:Not Important]",
        "[Sender:Vyssyni]       [Status:Not Important]",
        "[Sender:Inter-planetary Mining Management of the UED]       [Status:Mass-Warning]",
        "[Sender:Vyssyni]       [Status:Warning]",
        "[Sender:Vyssyni]       [Status:Not Important]",
        "[Sender:Inter-planetary Mining Management of the UED]       [Status:Mission File]",
        "[Sender:Vyssyni]       [Status:Not Important]",
    };
    public string[] BodyText = new string[]
    {
        "Message:Welcome, Engineer 0243. You have been sent to this place with a simple mission: We need you to establish an outpost and harvest natural"+
    "resources available in the area. Be wary though, we have detected lower life forms near your deposit which will be atracted to you due to genrator"+
    "phase emision. As they might put your mission in danger, we require you to eliminate them if any were to appear.  >>Vyssini<< will be your patron for"+
    "the duration of your mission. Any failure will effect in your immidiate dismissal. Good luck!",

        "Message:Hi there! My name is Vyssyni and I will be your patron for the rest of your mission. If there is a need, I will contact you."+
    "#$% as if I were allowed to help you %$# Work Good for Earths sake! "+
    "#$% You were denied acces to a new generator, but dont worry, I will lend you one. You owe me ;). Have fun, pal%#$ ",

        "Message: Expand you base #$% You know, most of you are send here to die out failing you mission. Thats what they do, but I am happy you survived this"+
    "long. Keep up the good work, will message you soon. :*%$",

        "Message:UED HQ#$%FUCKERS%$# seems to be happy with your work till now. I should also inform you that half of Engineers sent already failed at their mission."+
    "You must not!#$% who are they trying to fool? would be funny if it wasnt so tragic. Treating life as fuel for the machine and remove any parts that threaten its work. You know, I once had a sister. "+
    "She was asked for questioning to the main command of my district. Naturaly, I have never seen her again. We need to value ourselves%$#",

        "Message:To your information, there was an escape attempt carried by one of engineer in your designated working area. He was quickly captured and awaits his trail in UED"+
    "worker court. We hope that you will not try to betray Earth and act against humanity with similar actions. Continue on with your work.",

        "Message: I assume you have heard about recent events #$% I tried to help him,but it was for no good %$#. I am going to make sure you are not going to repeat his mistakes.",

        "Message:I should warn you about the increased rate of unknown hostile race moving towards you. Be wary and eliminate them #$% You know what are those? They are their failure. They have tried"+
    "to creat a new obedient species, which they would use to terraform planets. Sounds crazy, like some old time 21 century fantasy, right? Of course, it failed and now you are send here to deal with it,"+
    "making you and them anihilate each-other. Two birds with one stone. But you, please, prevail. I belive in you %$#",

        "Message:I seems that you have established a stabel outpost. This endevor exeeded out expectations and there is a possibility you are going to be reworded for your work. For know focus on keeping"+
    "your base in good shape till you get new mission files.",

        "Message: I want to officialy congratulate you #$% and say goodbye %$# for your endevors. If you keep up the paste for the sake of humanity, you will surly go far. Great job! #$% they found out that"+
    "I was helping engineers to free themselves from UED talons. I wanted to help you, but it doesnt seem possible anymore. I want you to know I enyojed the little connection we had, even if it was one sided."+
    "I will be on the run, hoping they wont catch up. You will never hear from me again. Last request: live. For your sake and the sake of others. And NEVER trust UED. Well, good bye :) %$#"

    };
    public float[] TimeMarkers = new float[] { 10, 20, 30, 40, 50, 60, 70, 80, 90 };


    public void NextMessage()
    {
        SenderTextDisplayer.text = SenderText;
        HeaderTextDisplayer.text = HeaderText[MSGDisplayedNumber];
        ContentTextDisplayer.text = BodyText[MSGDisplayedNumber];
        MSGDisplayedNumber++;
    }

    private void Start()
    {
        NextMessage();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > TimeMarkers[MSGDisplayedNumber]) { Debug.LogError("NextMessage");     NextMessage(); }
    }
}
