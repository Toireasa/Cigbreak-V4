using UnityEngine;
using System.Collections;
using UnityEditor;
using CigBreak;

public static class CreateScriptableObject
{
    [MenuItem("Cig Break/In Game/Game Item Data [SO]")]
    public static void CreateGameItemData()
    {
        ScriptableObjectUtility.CreateAsset<GameItemData>();
    }

    [MenuItem("Cig Break/In Game/Game Item Set [SO]")]
    public static void CreateGameItemSet()
    {
        ScriptableObjectUtility.CreateAsset<GameItemSet>();
    }

    [MenuItem("Cig Break/Profile/Reasons to Quit List [SO]", priority = 0)]
    public static void CreateReasonsToQuitList()
    {
        ScriptableObjectUtility.CreateAsset<ReasonsToQuitSet>();
    }

    [MenuItem("Cig Break/Profile/Reason to Quit [SO]", priority = 1)]
    public static void CreateReasonToQuit()
    {
        ScriptableObjectUtility.CreateAsset<ReasonToQuit>();
    }

    [MenuItem("Cig Break/Profile/Subreason to Quit [SO]", priority = 2)]
    public static void CreateSubreasonToQuit()
    {
        ScriptableObjectUtility.CreateAsset<SubreasonToQuit>();
    }

    [MenuItem("Cig Break/Profile/Smoking Habits List [SO]", priority = 21)]
    public static void CreateSmokingHabitsList()
    {
        ScriptableObjectUtility.CreateAsset<SmokingHabits>();
    }

    [MenuItem("Cig Break/Profile/Quit Methods List [SO]", priority = 41)]
    public static void CreateQuitMethodList()
    {
        ScriptableObjectUtility.CreateAsset<QuitMethods>();
    }

    [MenuItem("Cig Break/Profile/Badge Set [SO]", priority = 61)]
    public static void CreateBadgeSet()
    {
        ScriptableObjectUtility.CreateAsset<BadgeSet>();
    }

    [MenuItem("Cig Break/Profile/Badge [SO]", priority = 62)]
    public static void CreateBadge()
    {
        ScriptableObjectUtility.CreateAsset<Badge>();
    }

    [MenuItem("Cig Break/Profile/Veg Reward [SO]", priority = 63)]
    public static void CreateVegReward()
    {
        ScriptableObjectUtility.CreateAsset<VegReward>();
    }

    [MenuItem("Cig Break/Levels/Level Set [SO]", priority = 81)]
    public static void CreateLevelSet()
    {
        ScriptableObjectUtility.CreateAsset<LevelSet>();
    }

    [MenuItem("Cig Break/Levels/Level Data [SO]", priority = 82)]
    public static void CreateLevelData()
    {
        ScriptableObjectUtility.CreateAsset<LevelData>();
    }

    [MenuItem("Cig Break/Levels/Tutorial [SO]", priority = 83)]
    public static void CreateTutorialData()
    {
        ScriptableObjectUtility.CreateAsset<TutorialData>();
    }

    [MenuItem("Cig Break/Levels/FailTutorial [SO]", priority = 84)]
    public static void CreateFailTutorial()
    {
        ScriptableObjectUtility.CreateAsset<FailTutorial>();
    }

    [MenuItem("Cig Break/Levels/Level Rewards [SO]", priority = 85)]
    public static void CreateLevelReward()
    {
        ScriptableObjectUtility.CreateAsset<LevelRewards>();
    }

    [MenuItem("Cig Break/Journal/Tasks [SO]", priority = 200)]
    public static void CreateTask()
    {
        ScriptableObjectUtility.CreateAsset<TasksData>();
    }

    [MenuItem("Cig Break/Journal/SetTasks [SO]", priority = 201)]
    public static void CreateTaskSet()
    {
        ScriptableObjectUtility.CreateAsset<TaskSet>();
    }

    [MenuItem("Cig Break/Journal/HealthData [SO]", priority = 202)]
    public static void CreateHealthData()
    {
        ScriptableObjectUtility.CreateAsset<HealthData>();
    }

    [MenuItem("Cig Break/Journal/HeakthDataSet [SO]", priority = 203)]
    public static void CreateHealthDataSet()
    {
        ScriptableObjectUtility.CreateAsset<HealthDataSet>();
    }

    [MenuItem("Cig Break/Journal/BadgeData [SO]", priority = 204)]
    public static void CreateBadgeDatat()
    {
        ScriptableObjectUtility.CreateAsset<BadgeData>();
    }

    [MenuItem("Cig Break/Journal/BadgeDataSet [SO]", priority = 205)]
    public static void CreateBadgeDatatSet()
    {
        ScriptableObjectUtility.CreateAsset<BadgeDataSet>();
    }

}
