namespace Seti
{
    // View type
    public enum ViewType
    {
        Follow_Person,
        QuaterView,
    }

    public enum Stance
    {
        Normal,
        Board,
        Boots
    }

    public enum Action
    {
        Idle,
        Walk,
        Run,
        Dash,
        Drive
    }

    public enum Type_UI
    {
        MacroMECH_Receiver,
        MacroMECH_Transducer,
        MacroMECH_Propulsor,
    }

    public enum Type_AI
    {
        Storyteller,
        MacroMECH,
        Mechanic,
        Designer,
        Rider,
    }

    public enum Type_Quest
    {
        None,
        MainStory,
        NormalQuest,
        ChainQuest,
    }

    public enum GearType
    {
        Board,
        Boots
    }

    public enum BoardDirection
    {
        Left,
        Right,
        Null
    }
}