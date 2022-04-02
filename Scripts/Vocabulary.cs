using System.Collections.Generic;
using UnityEngine;

public class Vocabulary
{
        public int MaxLength = 5;
        public readonly List<string> Terms = new List<string>
        {
                "day","sky","sun","mir","mare","nasa","nova","mass","mars","moon","dust","lens","star",
                "space","nadir","deneb","comet","orbit","earth","phase","umbra","probe","rings","flyby",
                "lunar","flare","solar","pluto","venus","galaxy","zenith","helium","parsec","nebula",
                "planet","pulsar","quasar","rocket","saturn","syzygy","uranus","vacuum","waning",
                "waxing","x-rays","meteor","albedo","zodiac","apogee","corona","cosmos","crater","gravity",
                "sunspot","density","sputnik","cluster","azimuth","mercury","synodic","revolve","radiant",
                "muttnik","perigee","neptune","jupiter","transit","inertia","eclipse","equinox","docking",
                "ecliptic","aperture","apastron","wormhole","aphelion","sidereal","penumbra","new moon",
                "solstice","universe","spectrum","asteroid","asterism","hydrogen","parallax","cislunar",
                "totality","telescope","magnitude","pole star","gamma ray","cosmonaut","bolometer","meteorite",
                "meteoroid","milky way","planetoid","cosmology","local arm","starlight","telemetry","red dwarf",
                "hypernova","ice giant","twinkling","astronomy","free fall","half moon","radiation","supernova",
                "exoplanet","full moon","satellite","astronaut","gas giant","celestial","red shift","perihelion",
                "translunar","precession","black hole","black body","axial tilt","solar wind","astronomer",
                "terminator","wavelength","opposition","dwarf star","kiloparsec","ionosphere","oort cloud",
                "deep space","north star","earthbound","light-year","singularity","kuiper belt","binary star",
                "inclination","local group","declination","dark matter","roche limit","h-r diagram","cosmic rays",
                "observatory","white giant","white dwarf","conjunction","gravitation","occultation","gegenschein",
                "terrestrial","double star","falling star","spectroscope","gibbous moon","hubble's law",
                "solar system","yellow dwarf","heliocentric","eccentricity","interstellar","dwarf planet",
                "perturbation","minor planet","neutron star","quarter moon","crescent moon","doppler shift",
                "geostationary","total eclipse","event horizon","variable star","meteor shower","constellation",
                "extragalactic","scintillation","inner planets","shooting star","kepler's laws","kirkwood gaps",
                "outer planets","space station","drake equation","coriolis force","transneptunian",
                "geosynchronous","bailey's beads","alpha centauri","vernal equinox","red giant star",
                "weightlessness","van allen belt","escape velocity","right ascension","big bang theory",
                "partial eclipse","lagrange points","visual magnitude","elliptical orbit","planetary nebula",
                "globular cluster","superior planets","hubble telescope","hyperbolic orbit","inferior planets",
                "astronomical unit","space exploration","interstellar dust","celestial equator",
                "gravitational lens","scientific notation","orbital inclination","orbital eccentricity",
                "background radiation","plane of the ecliptic","gravitational constant"
        };

        public string GetRandomTerm()
        {
                var validTerms = Terms.FindAll(term => term.Length <= MaxLength);
                return validTerms[Random.Range(0, validTerms.Count)];
        }
}