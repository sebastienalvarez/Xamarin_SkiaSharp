/****************************************************************************************************************************************
 * 
 * Enum TouchActionType
 * Auteur : S. ALVAREZ
 * Date : 09-08-2020
 * Statut : En test
 * Version : 1
 * Revisions : NA
 *  
 * Objet : Enumération définissant les différents types d'actions possibles sur l'écran tactile. Ces types d'actions correspondent 
 *         aux événements à traiter. 
 * 
 ****************************************************************************************************************************************/

namespace TouchTracking
{
    public enum TouchActionType
    {
        Entered,
        Pressed,
        Moved,
        Released,
        Exited,
        Cancelled
    }
}
