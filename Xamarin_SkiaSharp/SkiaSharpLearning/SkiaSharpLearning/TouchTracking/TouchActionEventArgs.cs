/****************************************************************************************************************************************
 * 
 * Classe TouchActionEventArgs
 * Auteur : S. ALVAREZ
 * Date : 09-08-2020
 * Statut : En test
 * Version : 1
 * Revisions : NA
 *  
 * Objet : Classe représentant les informations envoyées lors d'un événement sur l'écran tactile.
 * 
 ****************************************************************************************************************************************/

using Xamarin.Forms;

namespace TouchTracking
{
    public class TouchActionEventArgs
    {
        // PROPRIETES
        /// <summary>
        /// Identifiant unique de l'action sur l'écran tactile
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Type de l'action effectuée sur l'écran tactile
        /// </summary>
        public TouchActionType Type { get; private set; }

        /// <summary>
        /// Point d'application de l'action sur l'écran tactile
        /// </summary>
        public Point Location { get; private set; }

        /// <summary>
        /// Flag indiquant si le doigt de l'utilisateur est en contact avec l'écran tactile pour l'action considérée
        /// </summary>
        public bool IsInContact { get; private set; }

        // CONSTRUCTEUR
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="id">Identifiant unique de l'action sur l'écran tactile</param>
        /// <param name="type">Type de l'action effectuée sur l'écran tactile</param>
        /// <param name="location">Point d'application de l'action sur l'écran tactile</param>
        /// <param name="isInContact">Flag indiquant si le doigt de l'utilisateur est en contact avec l'écran tactile pour l'action considérée</param>
        public TouchActionEventArgs(long id, TouchActionType type, Point location, bool isInContact)
        {
            Id = id;
            Type = type;
            Location = location;
            IsInContact = isInContact;
        }

        // METHODES
        // Pas de méthodes

    }
}
