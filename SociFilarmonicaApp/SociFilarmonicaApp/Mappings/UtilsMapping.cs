﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Mappings
{
    public static class UtilsMapping
    {
        public static ViewModels.SocioVm ToSocioVm(this Models.Socio socio)
        {
            ViewModels.SocioVm s = new ViewModels.SocioVm()
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
                TipoAutoDesc = socio.DatiAuto?.TipoAuto,
                TipoAutoID = socio.DatiAutoID,
                TipologiaSocioDesc = socio.Tipologia?.Descrizione,
                TipologiaSocioID = socio.TipologiaSocioID,
                DataNascita = socio.DataNascita,
                LuogoNascita = socio.LuogoNascita
            };
            return s;
        }
    }
}
