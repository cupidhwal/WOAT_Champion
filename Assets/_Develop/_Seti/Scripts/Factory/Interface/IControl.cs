namespace Seti
{
    /// <summary>
    /// Control Interface
    /// </summary>
    public interface IControl
    {
        void OnEnter(Actor actor);  // �ʱ�ȭ
        void OnExit(Actor actor);   // ����
    }
}