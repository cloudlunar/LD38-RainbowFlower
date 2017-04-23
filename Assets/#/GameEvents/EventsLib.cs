using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventsLib : MonoBehaviour
{
    public static int WinC
    {
        get
        {
            var g = 0;
            if (PlayerPrefs.HasKey("Win"))
                g = PlayerPrefs.GetInt("Win");
            return g;
        }
        set
        {
            PlayerPrefs.SetInt("Win", value);
        }
    }
    public static GameEvent[] lib=new[]{
        //Note系列:纸条可能发生一些未知的数值变动//
        //0// 
        new GameEvent{
            title="Ending",
            msg="You've successfully found one petal of Rainbow Flower",
            InvokeFunc=(x)=>
            {
                var i=x.name[x.name.Length-1]-'0';
                WinC|= 1<<i;
                AudioManager.ins.PlayAudio("A/s" + PressToStart.nowEnd);
            },
            callback=()=>
            {
                SceneManager.LoadScene(0);
            }
        }
    ,
        //20个Omen//
        //1// 
        new GameEvent{
            title="Omen",
            msg="In the small world, there are seven petals of Rainbow Flower",
            inta=1
        },
        //2// 
        new GameEvent{
            title="Omen",
            msg="I am so cutttttttttteeeeeeeee~~",
            sana=2,inta=-1
        },
        //3// 
        new GameEvent{
            title="Omen",
            msg="Death will come",
            hpa=1f
        },
        //4// 
        new GameEvent{
            title="Omen",
            msg="I can see forever",
            sana=0.5f,inta=0.5f
        },
        //5// 
        new GameEvent{
            title="Omen",
            msg="I cannot see forever",
            sana=-0.5f,inta=-0.5f
        },
        //6// 
        new GameEvent{
            title="Omen",
            msg="Something strange happened",
            sana=-1f
        },
        //7// 
        new GameEvent{
            title="Omen",
            msg="You feel blessed",
            sana=1f
        },
        //8// 
        new GameEvent{
            title="Omen",
            msg="Draaagggggggon!!! There's a dragon in this small world",
            sana=-1f
        },
        //9// 
        new GameEvent{
            title="Omen",
            msg="Nothing happened",
        },
        //10// 
        new GameEvent{
            title="Omen",
            msg="Heroes will never die",
            hpa=3
        },
        //11// 
        new GameEvent{
            title="Omen",
            msg="Take a try",
            inta=1,hpa=1
        },
        //12// 
        new GameEvent{
            title="Omen",
            msg="Do not let yourself become a person unlike yourself",
            sana=1
        },
        //13// 
        new GameEvent{
            title="Omen",
            msg="Hahahahahahaha",
            hpa=-1f
        },
        //14// 
        new GameEvent{
            title="Omen",
            msg="Try to spend some time",
            sana=0.5f,inta=0.5f
        },
        //15// 
        new GameEvent{
            title="Omen",
            msg="Think more",
            inta=3
        },
        //16// 
        new GameEvent{
            title="Omen",
            msg="Run!!!",
            sana=-1f,vla=3
        },
        //17// 
        new GameEvent{
            title="Omen",
            msg="Trust yourself",
            sana=1f
        },
        //18// 
        new GameEvent{
            title="Omen",
            msg="You have see something but you forget",
            sana=-1f
        },
        //19// 
        new GameEvent{
            title="Omen",
            msg="MEOW~"
        },
        //20// 
        new GameEvent{
            title="Omen",
            msg="ONE TWO THREE"            
        },
        
        //20个Note//
        //21// 
        new GameEvent{
            title="Note",
            msg="<i>\"All things in their being are good for something.\"</i>",
            inta=1,sana=1,vla=1,hpa=1
        },
        //22// 
        new GameEvent{
            title="Note",
            msg="<i>\"Difficult circumstances serve as a textbook of life for people.\"</i>",
            inta=1
        },
        //23// 
        new GameEvent{
            title="Note",
            msg="<i>\"For man is man and master of his fate.\"</i>",
            sana=1
        },
        //24// 
        new GameEvent{
            title="Note",
            msg="<i>\"The unexamined life is not worth living.——Socrates\"</i>",
            sana=3
        },
        //25// 
        new GameEvent{
            title="Note",
            msg="<i>\"None is of freedom or of life deserving unless he daily conquers it anew.——Erasmus\"</i>",
            sana=3
        },
        //26// 
        new GameEvent{
            title="Note",
            msg="<i>\"Autumn is a second Spring when every leaf is a flower.\"</i>",
            sana=1,inta=1
        },
        //27// 
        new GameEvent{
            title="Note",
            msg="<i>\"Adults are just obsolete children and the hell with them.\"</i>",
            sana=1
        },
        //28// 
        new GameEvent{
            title="Note",
            msg="<i>\"Be yourself; everyone else is already taken.\"</i>",
            sana=1,hpa=1,vla=0.5f
        },
        //29// 
        new GameEvent{
            title="Note",
            msg="<i>\"Well behaved men rarely make history.\"</i>",
            inta=2
        },
        //30// 
        new GameEvent{
            title="Note",
            msg="<i>\"UP UP DOWN DOWN LEFT RIGHT LEFT RIGHT A A B B SELECT START\"</i>",
            sana=1,inta=3,hpa=3,vla=1
        },
        //31// 
        new GameEvent{
            title="Note",
            msg="<i>\"Survival or death, that is a problem\"</i>",
            hpa=1
        },
        //32// 
        new GameEvent{
            title="Note",
            msg="<i>\"*#*##*#*\"</i>"
            
        },
        //33// 
        new GameEvent{
            title="Note",
            msg="<i>\"$30\"</i>"
            
        },
        //34// 
        new GameEvent{
            title="Note",
            msg="<i>\"K8FAS-L34SF-I1I0O\"</i>"
        },
        //35// 
        new GameEvent{
            title="Note",
            msg="<i>\"20160423\"</i>"
            
        },
        //36// 
        new GameEvent{
            title="Note",
            msg="<i>\"OOXXXXOOOOXXXXOO\"</i>",
            sana=1
        },
        //37// 
        new GameEvent{
            title="Note",
            msg="<i>\"[Photo]\"</i>",
            sana=-0.5f,inta=1f
        },
        //38// 
        new GameEvent{
            title="Note",
            msg="<i>\"[Video]\"</i>",
            sana=-0.5f,inta=1f
        },
        //39// 
        new GameEvent{
            title="Note",
            msg="<i>\"A small world\"</i>"
        
        },
        //40// 
        new GameEvent{
            title="Note",
            msg="<i>\"Let me think——Junyang Xiao\"</i>",
            sana=1,inta=3,hpa=3,vla=1
        },
    };

}
public class GameEvent
{
    public delegate void VoidDeg(GameObject gb);
    public VoidDeg InvokeFunc=(x)=> { };
    public void Invoke(GameObject gb)
    {
        if (Creature.player.HP < hpn || Creature.player.SAN < sann || Creature.player.VL < vln || Creature.player.INT < intn || Creature.player.LV < lvn)
        {
            if(!string.IsNullOrEmpty(msgfailed))
                MsgUI.Show(msgfailed, title);
            return;
        }
        InvokeFunc(gb);
        Creature.player.HP = Mathf.Clamp(Creature.player.HP + hpa, 0, 10);
        Creature.player.VL = Mathf.Clamp(Creature.player.VL + vla, 0, 10);
        Creature.player.INT = Mathf.Clamp(Creature.player.INT + inta, 0, 10);
        Creature.player.SAN = Mathf.Clamp(Creature.player.SAN + sana, 0, 10);
        Creature.player.LV = Mathf.Max(lvt, Creature.player.LV);
        foreach (var x in abilities)
            if(!Creature.player.abilities.Contains(x))
            Creature.player.abilities.Add(x);
         ShowMsg();
    }
    public string title = "Event";
    public string msg = "";
    public string msgfailed = "";
    public MsgUI.VoidDeg callback;
    public float hpn=0, sann=0, vln=0, intn=0,lvn=0;
    public float hpa, sana, vla, inta,lvt;
    public string[] abilities=new string[] { };
    public string parseFloat(float x)
    {
        string r = ((int)(x * 2) / 2f).ToString();
        if (x > 0) r = "+" + r;
        return r;
    }
    void ShowMsg() { 
            var s = msg+ "<size=21><i><color=#495685FF>\n";
            if (hpa != 0)
                s += "STR" + parseFloat(hpa)+" ";
            if (sana != 0)
                s += "SAN" + parseFloat(sana) + " ";
            if (vla != 0)
                s += "VLT" + parseFloat(vla) + " ";
            if (inta != 0)
                s += "INT" + parseFloat(inta) + " ";

            MsgUI.Show( s+"</color></i></size>",title,callback);
    }

}
