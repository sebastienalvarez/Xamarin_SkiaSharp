/****************************************************************************************************************************************
 * 
 * Classe TouchEffect
 * Auteur : S. ALVAREZ
 * Date : 09-08-2020
 * Statut : En test
 * Version : 1
 * Revisions : NA
 *  
 * Objet : Classe représentant l'effet pour la gestion des gestes sur l'écran tactile.
 * 
 ****************************************************************************************************************************************/

using Xamarin.Forms;

namespace TouchTracking
{
    public class TouchEffect : RoutingEffect
    {
        // PROPRIETES
        /// <summary>
        /// Flag indiquant le comportenement des events (à fixer à true)
        /// </summary>
        public bool Capture { get; set; }

        // EVENEMENTS
        /// <summary>
        /// Délégué pour la gestion d'un événement lié à une action sur l'écran tactile
        /// </summary>
        /// <param name="sender">Element Xamarin.Forms ayant levé l'événement</param>
        /// <param name="args">Informations sur l'action ayant levé l'évenement</param>
        public delegate void TouchActionEventHandler(object sender, TouchActionEventArgs args);
        
        /// <summary>
        /// Evenement lié à une action sur l'écran tactile
        /// </summary>
        public event TouchActionEventHandler TouchAction;

        // CONSTRUCTEUR
        /// <summary>
        /// Constructeur
        /// </summary>
        public TouchEffect() : base("XamarinDocs.TouchEffect")
        {
        }

        // METHODES
        /// <summary>
        /// Lève un évenement TouchAction
        /// </summary>
        /// <param name="element">Element Xamarin.Forms ayant levé l'évenement</param>
        /// <param name="args">Informations sur l'action ayant levé l'évenement</param>
        public void OnTouchAction(Element element, TouchActionEventArgs args)
        {
            TouchAction?.Invoke(element, args);
        }

    }
}
