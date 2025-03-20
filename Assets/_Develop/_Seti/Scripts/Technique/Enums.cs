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
        Drive,
    }

    public enum Interaction
    {
        Idle,
        Dialogue,
        Choice,
        Action
    }

    // 세대 추가 : Parts
    public enum Generation
    {
        Gen1,
        Gen2,
    }

    public enum Type_Interaction
    {
        Trade,
        Modify,
        Dialogue,
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

    public enum Type_Gear
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