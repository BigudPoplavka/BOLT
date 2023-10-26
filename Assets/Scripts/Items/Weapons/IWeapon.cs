public interface IWeapon
{
    public string Name { get; set; }
    bool CanAttack();
    void Attack();
}
