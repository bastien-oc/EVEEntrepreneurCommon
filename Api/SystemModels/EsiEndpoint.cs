using System;
using System.Linq;

namespace EntrepreneurCommon.Api.SystemModels
{
    public class EsiEndpoint
    {
        private string endpoint_template;
        private string endpoint_version;

        public string EndpointTemplate { get => endpoint_template; set => endpoint_template = value; }
        public string EndpointVersion { get => endpoint_version; set => endpoint_version = value; }

        private Int32 characterID;
        private Int32 corporationID;
        private Int64 observerID;
        private Int32 allianceID;
        private Int32 eventID;
        private Int32 contractID;
        private Int32 outpostID;
        private Int64 starbaseID;
        private Int64 structureID;
        private Int32 attributeID;
        private Int32 effectID;
        private Int32 fittingID;
        private Int64 fleetID;
        private Int64 wingID;
        private Int64 squadID;
        private Int32 memberID;
        private Int32 killmailID;
        private String killmailHash;
        private Int64 mailID;
        private Int32 regionID;
        private Int32 marketGroupID;
        private Int32 taskID;
        private Int32 planetID;
        private Int32 schematicID;
        private Int32 destination;
        private Int32 origin;
        private Int32 categoryID;
        private Int32 constellationID;
        private Int32 stationID;
        private Int32 stargateID;
        private Int32 starID;
        private Int32 moonID;
        private Int32 typeID;
        private Int32 division;
        private Int32 warID;

        [EndpointAttribute("character_id")] public int CharacterID { get => characterID; set => characterID = value; }
        [EndpointAttribute("corporation_id")] public int CorporationID { get => corporationID; set => corporationID = value; }
        [EndpointAttribute("alliance_id")] public int AllianceID { get => allianceID; set => allianceID = value; }

        [EndpointAttribute("station_id")] public int StationID { get => stationID; set => stationID = value; }
        [EndpointAttribute("outpost_id")] public int OutpostID { get => outpostID; set => outpostID = value; }
        [EndpointAttribute("starbase_id")] public long StarbaseID { get => starbaseID; set => starbaseID = value; }
        [EndpointAttribute("structure_id")] public long StructureID { get => structureID; set => structureID = value; }
        [EndpointAttribute("observer_id")] public long ObserverID { get => observerID; set => observerID = value; }

        [EndpointAttribute("fleet_id")] public long FleetID { get => fleetID; set => fleetID = value; }
        [EndpointAttribute("wing_id")] public long WingID { get => wingID; set => wingID = value; }
        [EndpointAttribute("squad_id")] public long SquadID { get => squadID; set => squadID = value; }
        [EndpointAttribute("member_id")] public int MemberID { get => memberID; set => memberID = value; }

        [EndpointAttribute("planet_id")] public int PlanetID { get => planetID; set => planetID = value; }
        [EndpointAttribute("star_id")] public int StarID { get => starID; set => starID = value; }
        [EndpointAttribute("moon_id")] public int MoonID { get => moonID; set => moonID = value; }

        [EndpointAttribute("region_id")] public int RegionID { get => regionID; set => regionID = value; }
        [EndpointAttribute("constellation_id")] public int ConstellationID { get => constellationID; set => constellationID = value; }
        [EndpointAttribute("stargate_id")] public int StargateID { get => stargateID; set => stargateID = value; }

        [EndpointAttribute("category_id")] public int CategoryID { get => categoryID; set => categoryID = value; }
        [EndpointAttribute("market_group_id")] public int MarketGroupID { get => marketGroupID; set => marketGroupID = value; }
        [EndpointAttribute("type_id")] public int TypeID { get => typeID; set => typeID = value; }

        [EndpointAttribute("destination")] public int Destination { get => destination; set => destination = value; }
        [EndpointAttribute("origin")] public int Origin { get => origin; set => origin = value; }

        [EndpointAttribute("event_it")] public int EventID { get => eventID; set => eventID = value; }
        [EndpointAttribute("contract_id")] public int ContractID { get => contractID; set => contractID = value; }
        [EndpointAttribute("attribute_id")] public int AttributeID { get => attributeID; set => attributeID = value; }
        [EndpointAttribute("effect_id")] public int EffectID { get => effectID; set => effectID = value; }
        [EndpointAttribute("fitting_id")] public int FittingID { get => fittingID; set => fittingID = value; }
        [EndpointAttribute("killmail_id")] public int KillmailID { get => killmailID; set => killmailID = value; }
        [EndpointAttribute("killmail_hash")] public string KillmailHash { get => killmailHash; set => killmailHash = value; }
        [EndpointAttribute("mail_id")] public long MailID { get => mailID; set => mailID = value; }
        [EndpointAttribute("task_id")] public int TaskID { get => taskID; set => taskID = value; }
        [EndpointAttribute("schematic_id")] public int SchematicID { get => schematicID; set => schematicID = value; }
        [EndpointAttribute("division")] public int Division { get => division; set => division = value; }
        [EndpointAttribute("war_id")] public int WarID { get => warID; set => warID = value; }


        private string finalString;

        public EsiEndpoint( string Path )
        {
            this.endpoint_template = Path;
            this.finalString = Path;
        }

        public override string ToString()
        {
            finalString = endpoint_template;
            var props = from p in this.GetType().GetProperties()
                        let attr = p.GetCustomAttributes(typeof(EndpointAttribute), true)
                        where attr.Length == 1
                        select new { Property = p, Attribute = attr.First() as EndpointAttribute };
            foreach (var p in props) {
                string find_str = $"{{{p.Attribute.Name}}}";
                string replace = $"{p.Property.GetValue(this)}";

                finalString = finalString.Replace(find_str, replace);
            }
            return finalString;
        }
    }
}
