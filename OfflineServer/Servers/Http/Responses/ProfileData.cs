﻿using System;
using System.Xml.Serialization;

namespace OfflineServer.Servers.Http.Responses
{
    [Serializable()]
    [XmlRoot("ProfileData", IsNullable = true)]
    public class ProfileData
    {
        [XmlElement("Badges")]
        public String badges = "";
        [XmlElement("Boost")]
        public Int32 boost = 0;
        [XmlElement("Cash")]
        public Int32 cash = 0;
        [XmlElement("IconIndex")]
        public Int16 iconIndex = 27;
        [XmlElement("Level")]
        public Int32 level = 0;
        [XmlElement("Motto")]
        public String motto = "";
        [XmlElement("Name")]
        public String name = "Debug";
        [XmlElement("PercentToLevel")]
        public Int32 percentToLevel = 0;
        [XmlElement("PersonaId")]
        public Int32 personaId = 0;
        [XmlElement("Rating")]
        public Int32 rating = 0;
        [XmlElement("Rep")]
        public Int32 rep = 0;
        [XmlElement("RepAtCurrentLevel")]
        public Int32 repAtCurrentLevel = 0;
        [XmlElement("ccar")]
        public String ccar = null;
        [XmlElement("Score")]
        public Int32 score = 0;
    }
}