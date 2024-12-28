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
        Operation  // 기계 조작
    }

    //우주선 현재 동작 상태
    public enum ShipState
    {
        None,
        WestTurret,
        EastTurret,
        SouthTurret,
        NorthTurret,
        YamatoCannon,
        Engine,
        Shield
    }

    //탄환 종류
    public enum BulletType
    {
        TurretBullet,
        YamatoMissile
    }

    // 오브젝트 구분을 위한 타입
    public enum EPoolObjectType
    {
        TurretBulletGroup,
        YamatoMissileGroup
    }
}