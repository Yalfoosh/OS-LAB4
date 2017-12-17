namespace LAB4
{
    //Pretpostavljamo da je za 1. zadatak tip dretve NoType.
    public enum TType
    {
        NoType = 0,
        FIFO = 1,
        RoundRobin = 2
    }

    public class Dretva
    {
        public ulong ID { get; set; }
        public ulong Birthtime { get; set; }
        public ulong Lifetime { get; set; }
        public ulong Priority { get; set; }
        public TType Type { get; set; }

        public Dretva(ref ulong maxID, ulong birthtime, ulong lifetime, ulong priority, TType type = TType.NoType)
        {
            ID = maxID++;
            Birthtime = birthtime;
            Lifetime = lifetime;
            Priority = priority;
            Type = type;
        }
        Dretva(ulong ID, ulong birth, ulong life, ulong priority, TType type = TType.NoType)
        {
            this.ID = ID;
            Birthtime = birth;
            Lifetime = life;
            Priority = priority;
            Type = type;
        }

        /// <summary>
        /// Služi za stvaranje duboke kopije objekta (ne moram razmišljati o referencama)
        /// </summary>
        /// <returns>Duboka kopija originalnog objekta</returns>
        public Dretva DeepCopy() => new Dretva(ID, Birthtime, Lifetime, Priority, Type);

        public override string ToString() => "ID: " + ID + " | prioritet: " + Priority + (Type != TType.NoType ? " | tip dretve: " + Type : "") + " | umire za: " + Lifetime + " s";

        /// <summary>
        /// Smanjuje život dretve za 1.
        /// </summary>
        /// <returns>Ako je novi život dretve 0, vraća true.</returns>
        public bool Tick() => --Lifetime == 0;
    }
}