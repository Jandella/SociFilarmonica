using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Mappings
{
    public static class UtilsMapping
    {
        public static Models.SocioVm ToSocioVm(this Data.DbModels.Socio socio)
        {
            Models.SocioVm s = new Models.SocioVm()
            {
                Annullato = socio.Annullato,
                Cap = socio.Cap,
                Citta = socio.Citta,
                CodiceFiscale = socio.CodiceFiscale,
                Cognome = socio.Cognome,
                DescrizioneMacchina = socio.DescrizioneMacchina,
                Email = socio.Email,
                Email2 = socio.Email2,
                ID = socio.ID,
                Indirizzo = socio.Indirizzo,
                Nome = socio.Nome,
                NumeroSocio = socio.NumeroSocio,
                NumeroTesseraAmbima = socio.NumeroTesseraAmbima,
                PrivacyFirmata = socio.PrivacyFirmata,
                TargaMacchina = socio.TargaMacchina,
                Telefono = socio.Telefono,
                Telefono2 = socio.Telefono2,
                DatiAutoID = socio.DatiAutoID,
                TipologiaSocioDesc = socio.Tipologia?.Descrizione,
                TipologiaSocioID = socio.TipologiaSocioID,
                DataNascita = socio.DataNascita,
                LuogoNascita = socio.LuogoNascita
            };
            if (socio.DatiAuto != null)
            {
                s.DatiAutoID = socio.DatiAutoID;
                s.TipoAutoDesc = $"{socio.DatiAuto.TipoAuto} - {socio.DatiAuto.Carburante}";
            }
            return s;
        }
    }
}
