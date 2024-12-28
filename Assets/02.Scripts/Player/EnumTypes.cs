namespace EnumTypes
{
    // 플레이어 상태
    public enum PlayerState
    {
        Idle,      // 기본
        Move,      // 이동
        Jump,      // 점프
        Crash,     // 충돌
        Climb,     // 사다리
        Operation, // 기계 조작
        Command    // AI 명령
    }
}