using UnityEngine;

//base class for all actions, also inherited by equipment? equipment has use function that also takes the visible equipment gameobject
[CreateAssetMenu(fileName = "SkillInfo", menuName ="Scriptable Objects/SkillInfo")]
public class SkillInfo : ScriptableObject
{
    //what will become a translation key
    public string SkillName;

    public Sprite SkillIcon;

    public SkillData SkillData;
}

//ALRIGHT! we pass this to reference a skill, they take the skillDATA and call its Use() function, passing their body. for equipment, we just replicate this structure

//bool SkillData.IsSkill<SkillData T>(int/enum skill, out T data) { if (this == Resources.GetSkill(skill)) return this as T;

//For body-specific animations (like firing from a gun):
//sol 1: if (body.Type == CommandoBody): (body as CommandoBody).dofireanim() or .gun.dofireanim()
//sol 2: if (body.TryGetComponent<CommandoGunAnimator>(out var gun) gun.do() or instantiate proj at gun.point