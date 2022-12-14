using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KouCoCoa
{
    internal partial class SkillEditor : Form
    {
        public SkillEditor(MobSkill skill)
        {
            _skill = skill;
            InitializeComponent();
            KouCoCoaInitialize();
        }

        public MobSkill _skill;

        private void KouCoCoaInitialize()
        {
            Text = $"{_skill.MobName} :: [{_skill.State}] {_skill.SkillName} {_skill.SkillLv} :: {Name}";
            InitializeToolTips();
            InitializeComboBoxValues();
            InitializeUpDown();

            mobIdTextBox.Text = _skill.MobId.ToString();
            mobNameTextBox.Text = _skill.MobName;
            skillNameTextBox.Text = _skill.SkillName;
            skillIdUpDown.Value = _skill.SkillId;
            skillLvlUpDown.Value = _skill.SkillLv;
            emotionTextBox.Text = _skill.Emotion;
            Utilities.SetComboBoxIndex(stateComboBox, _skill.State.ToString());
            castTimeUpDown.Value = _skill.CastTime;
            rateNumberUpDown.Value = _skill.Rate;
            delayUpDown.Value = _skill.Delay;
            Utilities.SetComboBoxIndex(targetComboBox, _skill.Target.ToString());
            Utilities.SetComboBoxIndex(cancelableComboBox, _skill.Cancelable.ToString());
            Utilities.SetComboBoxIndex(conditionTypeComboBox, _skill.ConditionType.ToString());
            conditionValueTextBox.Text = _skill.ConditionValue;
            val1TextBox.Text = _skill.Val1;
            val2TextBox.Text = _skill.Val2;
            val3TextBox.Text = _skill.Val3;
            val4TextBox.Text = _skill.Val4;
            val5TextBox.Text = _skill.Val5;
            chatTextBox.Text = _skill.Chat;
        }

        private void InitializeUpDown()
        {

            List<NumericUpDown> upDowns = new() {
                skillIdUpDown,
                skillLvlUpDown,
                rateNumberUpDown,
                castTimeUpDown,
                delayUpDown
            };
            foreach (NumericUpDown upDown in upDowns) {
                upDown.Minimum = 0;
                upDown.Maximum = int.MaxValue;
            }
        }

        private void InitializeToolTips()
        {
            ToolTip stateToolTip = new();
            stateToolTip.SetToolTip(stateLabel,
                "any (except dead) / idle (in standby) / walk (in movement) / dead (on killed) / " +
                "loot /attack / angry (like attack, except player has not attacked mob yet) " +
                "chase (following target, after being attacked) / follow (following target," +
                " without being attacked) / anytarget (attack+angry+chase+follow)");

            ToolTip rateToolTip = new();
            rateToolTip.SetToolTip(rateLabel, 
                "The chance of the skill being casted when the condition is fulfilled (10000 = 100%)");

            ToolTip delayToolTip = new();
            delayToolTip.SetToolTip(delayLabel,
                "The time (in milliseconds) before attempting to recast the same skill.");

            ToolTip targetToolTip = new();
            targetToolTip.SetToolTip(targetLabel,
                "target (current target) / self / friend / master / randomtarget (any enemy within skill's range)");
        }

        private void InitializeComboBoxValues()
        {
            foreach (MobState mobState in Enum.GetValues(typeof(MobState))) {
                stateComboBox.Items.Add(mobState.ToString());
            }
            foreach (MobSkillTarget skillTarget in Enum.GetValues(typeof(MobSkillTarget))) {
                targetComboBox.Items.Add(skillTarget.ToString());
            }
            foreach (MobSkillCancelable cancelable in Enum.GetValues(typeof(MobSkillCancelable))) {
                cancelableComboBox.Items.Add(cancelable.ToString());
            }
            foreach (MobSkillConditionType conditionType in Enum.GetValues(typeof(MobSkillConditionType))) {
                conditionTypeComboBox.Items.Add(conditionType.ToString());
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            _skill.SkillId = (int)skillIdUpDown.Value;
            _skill.SkillName = skillNameTextBox.Text;
            _skill.SkillLv = (int)skillLvlUpDown.Value;
            _skill.Emotion = emotionTextBox.Text;
            _skill.State = Enum.TryParse(stateComboBox.Text, out MobState mobState) ? mobState : _skill.State;
            _skill.CastTime = (int)castTimeUpDown.Value;
            _skill.Rate = (int)rateNumberUpDown.Value;
            _skill.Delay = (int)delayUpDown.Value;
            _skill.Target = Enum.TryParse(targetComboBox.Text, out MobSkillTarget skillTarget) ? skillTarget : _skill.Target;
            _skill.Cancelable = Enum.TryParse(cancelableComboBox.Text, out MobSkillCancelable cancelable) ? cancelable : _skill.Cancelable;
            _skill.ConditionType = Enum.TryParse(conditionTypeComboBox.Text, out MobSkillConditionType conditionType) ? conditionType : _skill.ConditionType;
            _skill.ConditionValue = conditionValueTextBox.Text;
            _skill.Val1 = val1TextBox.Text;
            _skill.Val2 = val2TextBox.Text;
            _skill.Val3 = val3TextBox.Text;
            _skill.Val4 = val4TextBox.Text;
            _skill.Val5 = val5TextBox.Text;
            _skill.Chat = chatTextBox.Text;
        }
    }
}
