using System.Windows.Forms;

namespace KouCoCoa
{
    internal static class Utilities
    {
        /// <summary>
        /// Basic string split function, returns characters between the provided delimiters in input string.
        /// </summary>
        internal static string StringSplit(string input, char firstDelimiter, char secondDelimiter)
        {
            string output = "";
            int indexTo = input.LastIndexOf(secondDelimiter);
            if (indexTo < 0) {
                indexTo = input.Length;
            }

            int indexFrom = input.LastIndexOf(firstDelimiter, indexTo - 1);
            if (indexFrom >= 0) {
                output = input.Substring(indexFrom + 1, indexTo - indexFrom - 1);
            }
            return output;
        }

        internal static void SetComboBoxIndex(ComboBox comboBox, string entry)
        {
            if (comboBox.Items.Contains(entry)) {
                int index = comboBox.Items.IndexOf(entry);
                comboBox.SelectedIndex = index;
                return;
            }
        }

        internal static string SpawnGroupToListEntry(SpawnGroup sg)
        {
            return $"[{sg.Id}] {sg.Name}";
        }

        internal static string MobToListEntry(Mob mob)
        {
            return $"[{mob.Id}] {mob.Name} ({mob.AegisName})";
        }

        internal static string SkillToListEntry(MobSkill skill)
        {
            return $"[{skill.State}] {skill.SkillName} (Lv. {skill.SkillLv})";
        }

        /// <summary>
        /// Yes, I'm lazy enough to be making a wrapper for TryParse.
        /// </summary>
        /// <param name="intToSet"></param>
        /// <param name="textBoxText"></param>
        /// <returns></returns>
        internal static int GetIntFromControl(TextBox textBox)
        {
            return int.TryParse(textBox.Text, out int x) ? x : int.MinValue;
        }

        internal static int GetIntFromControl(ComboBox comboBox)
        {
            return int.TryParse(comboBox.SelectedItem.ToString(), out int x) ? x : int.MinValue;
        }
    }
}
