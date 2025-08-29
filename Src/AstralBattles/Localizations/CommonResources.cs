using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using Windows.ApplicationModel.Resources;

namespace AstralBattles.Localizations
{
  public class CommonResources
  {
    private static ResourceLoader resourceLoader;

    static CommonResources()
    {
      // Initialize UWP ResourceLoader for the Strings folder
      try
      {
        resourceLoader = ResourceLoader.GetForViewIndependentUse("Resources");
      }
      catch
      {
        // Fallback if resource loading fails
        resourceLoader = null;
      }
    }

    private static string GetString(string key)
    {
      try
      {
        return resourceLoader?.GetString(key) ?? key;
      }
      catch
      {
        return key; // Fallback to key name if resource not found
      }
    }

    // Primary properties used in the UI
    public static string About => GetString(nameof(About));
    public static string AboutBody => GetString(nameof(AboutBody));
    public static string Air => GetString(nameof(Air));
    public static string Beast => GetString(nameof(Beast));
    public static string Campaign => GetString(nameof(Campaign));
    public static string CampaignTip => GetString(nameof(CampaignTip));
    public static string Casting => GetString(nameof(Casting));
    public static string Chaos => GetString(nameof(Chaos));
    public static string Close => GetString(nameof(Close));
    public static string ConfigureDeck => GetString(nameof(ConfigureDeck));
    public static string Confirmation => GetString(nameof(Confirmation));
    public static string Congratulations => GetString(nameof(Congratulations));
    public static string Continue => GetString(nameof(Continue));
    public static string ContinueCampaignTip => GetString(nameof(ContinueCampaignTip));
    public static string ContinueTip => GetString(nameof(ContinueTip));
    public static string ContinueTournamentTip => GetString(nameof(ContinueTournamentTip));
    public static string Control => GetString(nameof(Control));
    public static string Copyrights => GetString(nameof(Copyrights));
    public static string TwoPlayers => GetString(nameof(TwoPlayers));

    // Additional commonly used properties
    public static string CreatureShortDesc => GetString(nameof(CreatureShortDesc));
    public static string CreatureShortDesc2 => GetString(nameof(CreatureShortDesc2));
    public static string CurrentTournamentWillBeOverwritten => GetString(nameof(CurrentTournamentWillBeOverwritten));
    public static string Death => GetString(nameof(Death));
    public static string Demonic => GetString(nameof(Demonic));
    public static string Difficulty => GetString(nameof(Difficulty));
    public static string Earth => GetString(nameof(Earth));
    public static string Easy => GetString(nameof(Easy));
    public static string FaceSelection => GetString(nameof(FaceSelection));
    public static string Feedback => GetString(nameof(Feedback));
    public static string FifthElement => GetString(nameof(FifthElement));
    public static string Fire => GetString(nameof(Fire));
    public static string Game => GetString(nameof(Game));
    public static string GameWillBeOverwritten => GetString(nameof(GameWillBeOverwritten));
    public static string Goblins => GetString(nameof(Goblins));
    public static string Hard => GetString(nameof(Hard));
    public static string HasCasted => GetString(nameof(HasCasted));
    public static string HasSkipped => GetString(nameof(HasSkipped));
    public static string HasSummoned => GetString(nameof(HasSummoned));
    public static string HasWon => GetString(nameof(HasWon));
    public static string HelpImproveLanguages => GetString(nameof(HelpImproveLanguages));
    public static string Holy => GetString(nameof(Holy));
    public static string HostAServer => GetString(nameof(HostAServer));
    public static string HostAServerTip => GetString(nameof(HostAServerTip));
    public static string Illusion => GetString(nameof(Illusion));
    public static string IsComputer => GetString(nameof(IsComputer));
    public static string JoinToServer => GetString(nameof(JoinToServer));
    public static string JoinToServerTip => GetString(nameof(JoinToServerTip));
    public static string LoadGame => GetString(nameof(LoadGame));
    public static string LoadGameTip => GetString(nameof(LoadGameTip));
    public static string Loading => GetString(nameof(Loading));
    public static string MainMenu => GetString(nameof(MainMenu));
    public static string Mechanical => GetString(nameof(Mechanical));
    public static string Name => GetString(nameof(Name));
    public static string NamesShouldBeNotEmptyMessage => GetString(nameof(NamesShouldBeNotEmptyMessage));
    public static string NamesShouldBeNotEqualMessage => GetString(nameof(NamesShouldBeNotEqualMessage));
    public static string NewCampaign => GetString(nameof(NewCampaign));
    public static string NewCampaignTip => GetString(nameof(NewCampaignTip));
    public static string NewGame => GetString(nameof(NewGame));
    public static string NewGameTip => GetString(nameof(NewGameTip));
    public static string NextGame => GetString(nameof(NextGame));
    public static string NextTurn => GetString(nameof(NextTurn));
    public static string Normal => GetString(nameof(Normal));
    public static string ReviewGame => GetString(nameof(ReviewGame));
    public static string TapToChange => GetString(nameof(TapToChange));
    public static string Tournament => GetString(nameof(Tournament));
    public static string TournamentTip => GetString(nameof(TournamentTip));
    public static string TutorialTip => GetString(nameof(TutorialTip));
    public static string Vibration => GetString(nameof(Vibration));
    public static string ViaSockets => GetString(nameof(ViaSockets));
    public static string Water => GetString(nameof(Water));
    public static string Yes => GetString(nameof(Yes));
    public static string No => GetString(nameof(No));

    public static string You => GetString(nameof(You));
    public static string NextTurnMessage => GetString(nameof(NextTurnMessage));

    public static string RoundStarted => GetString(nameof(RoundStarted));



        // Specialization properties
    public static string Spec_Druid => GetString(nameof(Spec_Druid));

    public static string Spec_DruidTip => GetString(nameof(Spec_DruidTip));

    public static string Spec_Elementalist => GetString(nameof(Spec_Elementalist));
    public static string Spec_ElementalistTip => GetString(nameof(Spec_ElementalistTip));
    public static string Spec_Pyromancer => GetString(nameof(Spec_Pyromancer));
    public static string Spec_PyromancerTip => GetString(nameof(Spec_PyromancerTip));
    public static string Spec_Necromancer => GetString(nameof(Spec_Necromancer));
    public static string Spec_NecromancerTip => GetString(nameof(Spec_NecromancerTip));
    public static string Spec_IceLord => GetString(nameof(Spec_IceLord));
    public static string Spec_IceLordTip => GetString(nameof(Spec_IceLordTip));
    public static string Spec_Stormbringer => GetString(nameof(Spec_Stormbringer));
    public static string Spec_StormbringerTip => GetString(nameof(Spec_StormbringerTip));


    public static string Summoning => GetString(nameof(Summoning));


    // Player properties
    public static string Player1NameLabel => GetString(nameof(Player1NameLabel));
    public static string Player2NameLabel => GetString(nameof(Player2NameLabel));

    // Spell and creature related
    public static string Sorcery => GetString(nameof(Sorcery));
    public static string Undead => GetString(nameof(Undead));

    public static string SpellShortDesc => GetString(nameof(SpellShortDesc));
    public static string SpellShortDesc2 => GetString(nameof(SpellShortDesc2));

    // Additional game terms
    public static string Elements => GetString(nameof(Elements));
    public static string Skills => GetString(nameof(Skills));
    public static string Spells => GetString(nameof(Spells));
  }
}
