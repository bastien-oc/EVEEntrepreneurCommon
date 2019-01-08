using System;
using System.Linq;

namespace EntrepreneurCommon.Api.SystemModels
{
    [Obsolete("Obsolete tinkering with Endpoints", true)]
    public class EsiEndpointBase
    {
        //    private string finalString;

        //    public EsiEndpointBase(string Path)
        //    {
        //        EndpointTemplate = Path;
        //        finalString = Path;
        //    }

        //    public string EndpointTemplate { get; set; }

        //    public string EndpointVersion { get; set; }

        //    [EndpointAttribute("character_id")]   public int  CharacterID   { get; set; }
        //    [EndpointAttribute("corporation_id")] public int  CorporationID { get; set; }
        //    [EndpointAttribute("alliance_id")]    public int  AllianceID    { get; set; }
        //    [EndpointAttribute("station_id")]     public int  StationID     { get; set; }
        //    [EndpointAttribute("outpost_id")]     public int  OutpostID     { get; set; }
        //    [EndpointAttribute("starbase_id")]    public long StarbaseID    { get; set; }
        //    [EndpointAttribute("structure_id")]   public long StructureID   { get; set; }
        //    [EndpointAttribute("observer_id")]    public long ObserverID    { get; set; }
        //    [EndpointAttribute("fleet_id")]       public long FleetID       { get; set; }
        //    [EndpointAttribute("wing_id")]        public long WingID        { get; set; }
        //    [EndpointAttribute("squad_id")]       public long SquadID       { get; set; }
        //    [EndpointAttribute("member_id")]      public int  MemberID      { get; set; }
        //    [EndpointAttribute("planet_id")]      public int  PlanetID      { get; set; }
        //    [EndpointAttribute("star_id")]        public int  StarID        { get; set; }
        //    [EndpointAttribute("moon_id")]        public int  MoonID        { get; set; }
        //    [EndpointAttribute("region_id")]      public int  RegionID      { get; set; }

        //    [EndpointAttribute("constellation_id")]
        //    public int ConstellationID { get; set; }

        //    [EndpointAttribute("stargate_id")]     public int    StargateID    { get; set; }
        //    [EndpointAttribute("category_id")]     public int    CategoryID    { get; set; }
        //    [EndpointAttribute("market_group_id")] public int    MarketGroupID { get; set; }
        //    [EndpointAttribute("type_id")]         public int    TypeID        { get; set; }
        //    [EndpointAttribute("destination")]     public int    Destination   { get; set; }
        //    [EndpointAttribute("origin")]          public int    Origin        { get; set; }
        //    [EndpointAttribute("event_it")]        public int    EventID       { get; set; }
        //    [EndpointAttribute("contract_id")]     public int    ContractID    { get; set; }
        //    [EndpointAttribute("attribute_id")]    public int    AttributeID   { get; set; }
        //    [EndpointAttribute("effect_id")]       public int    EffectID      { get; set; }
        //    [EndpointAttribute("fitting_id")]      public int    FittingID     { get; set; }
        //    [EndpointAttribute("killmail_id")]     public int    KillmailID    { get; set; }
        //    [EndpointAttribute("killmail_hash")]   public string KillmailHash  { get; set; }
        //    [EndpointAttribute("mail_id")]         public long   MailID        { get; set; }
        //    [EndpointAttribute("task_id")]         public int    TaskID        { get; set; }
        //    [EndpointAttribute("schematic_id")]    public int    SchematicID   { get; set; }
        //    [EndpointAttribute("division")]        public int    Division      { get; set; }
        //    [EndpointAttribute("war_id")]          public int    WarID         { get; set; }

        //    public override string ToString()
        //    {
        //        finalString = EndpointTemplate;
        //        var props = from p in GetType().GetProperties()
        //                    let attr = p.GetCustomAttributes(typeof(EndpointAttribute), true)
        //                    where attr.Length == 1
        //                    select new {Property = p, Attribute = attr.First() as EndpointAttribute};
        //        foreach (var p in props) {
        //            var find_str = $"{{{p.Attribute.Name}}}";
        //            var replace  = $"{p.Property.GetValue(this)}";

        //            finalString = finalString.Replace(find_str, replace);
        //        }

        //        return finalString;
        //    }
    }
}
