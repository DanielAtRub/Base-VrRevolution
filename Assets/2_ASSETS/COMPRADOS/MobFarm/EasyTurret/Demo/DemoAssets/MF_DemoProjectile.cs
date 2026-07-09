using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mirror;

//namespace MobFarm {

	public class MF_DemoProjectile : NetworkBehaviour
    {
        [SerializeField] float destroyAfter = 1;
        [SerializeField] GameObject hitPrefab;
        [SerializeField] int daño = 1;

        //[Tooltip( "Damage of the projectile when it hits." )]
		//public float damage;

		[Header( "Fx:" )]
		[Tooltip( "How long fx items will remain after projectile is destroyed. Gives time for trail to finish and hit fx to display." )]
		[SerializeField] float fxDuration;
		[SerializeField] TrailRenderer trail;
		//[Tooltip( "Object that will appear when the projectil hits a collider." )]
		//[SerializeField] GameObject hitObject;

		[HideInInspector] public float duration;

		//Vector3 prevPosition1; // used with hit checking to compensate for fast motion
		//Vector3 prevPosition2; // used with hit checking to compensate for fast motion
		Rigidbody myRigidbody;
		//bool beginDestroy;
		//float startTime;

		//bool error;

        public override void OnStartServer()
        {
            Invoke(nameof(DestroySelf), destroyAfter);
        }

        // destroy for everyone on the server
        [Server]
        void DestroySelf()
        {
            NetworkServer.Destroy(gameObject);
        }

        [ServerCallback]
        void OnCollisionEnter(Collision co)
        {
            //EFECTO COLISION
            ContactPoint contact = co.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            if (hitPrefab != null)
            {
                var hitVFX = Instantiate(hitPrefab, pos, rot);
                NetworkServer.Spawn(hitVFX);
            }

            // Quita vida al player
            if (co.collider.tag == "Player")
                co.collider.GetComponentInParent<Player>().PlayerHealth -= daño;
            NetworkServer.Destroy(gameObject);
        }

        [ServerCallback]
        void Awake () {
			myRigidbody = GetComponent<Rigidbody>();

            myRigidbody.isKinematic = false; //PRUEBAS
            //if ( trail ) { trail.time = fxDuration; }
            //CheckErrors();
        }

        [ServerCallback]
        void OnEnable () {
			//if ( error == true ) { return; }
			//beginDestroy = false;
			//prevPosition1 = myRigidbody.position;
			//prevPosition2 = myRigidbody.position;
			//startTime = Time.time + Time.fixedDeltaTime; // effectively add 1 frame to duration - doing it this way because weapon script will change duration after this
														 // this helps with very fast projectiles ending early near max range due to time rounding errors
		}

        [ServerCallback]
        void OnDisable () {
			//if ( error == true ) { return; }

			// reset for object pooling
			myRigidbody.linearVelocity = Vector3.zero;
			myRigidbody.angularVelocity = Vector3.zero;
		}

        [ServerCallback]
        void FixedUpdate () {
			//if ( error == true || beginDestroy == true ) { return; }

			/*if ( Time.time >= startTime + duration ) {
				BeginDestroy(); // projectile death
			}*/

			// angle towards velocity
			if ( myRigidbody.linearVelocity != Vector3.zero ) {
				myRigidbody.rotation = Quaternion.LookRotation( myRigidbody.linearVelocity );
			}

			// check if shot is inside a collider - compensating for fast target movement
			/*RaycastHit hit = default( RaycastHit );
			Collider[] colliders = Physics.OverlapSphere( myRigidbody.position, 0f );
			if ( colliders != null && colliders.Length > 0 ) { // shot is inside a collider
															   // retrace movement to find hit point - looks from 2 positions back
				if ( Physics.Linecast( prevPosition2, myRigidbody.position, out hit ) ) {
					DoHit( hit );
				}
				// check for a hit - cast a ray to check hits along path - compensating for fast shot movement
			} else if ( Physics.Raycast( myRigidbody.position, myRigidbody.velocity, out hit, myRigidbody.velocity.magnitude * Time.fixedDeltaTime ) ) { // shot will hit a collider
				DoHit( hit );
			}*/
			// store positions for retracing
			//prevPosition2 = prevPosition1;
			//prevPosition1 = myRigidbody.position;
		}

        [Server]
        void DoHit ( RaycastHit hit ) { // hit a target
			transform.position = hit.point; // move to hit location
											// start hit Fx
			/*if ( hitObject ) {
				hitObject.transform.parent = null;
				hitObject.SetActive( true );
			}*/
			//DoDamage( hit.transform );
		}

        /*public void DoDamage ( Transform trans ) {
			// do stuff to the target object when it gets hit
			BeginDestroy(); // remove shot
		}*/

        /*public void BeginDestroy () {
			beginDestroy = true; // suppress further fixedUpdates
			//if ( trail || hitObject ) {
			//	if ( trail ) { trail.transform.parent = null; }
			//	Invoke( nameof( DestroyFx ), fxDuration );
			//} else {
				DoDestroy();
			//}
		}*/
        /*
		void DoDestroy () {
			// change this to support object pooling
			Destroy( gameObject );
		}
        
		void DestroyFx () {
			// change this to reattach to shot for object pooling
			if ( trail ) { Destroy( trail.gameObject ); }
			if ( hitObject ) { Destroy( hitObject ); }
			DoDestroy();
		}

		bool CheckErrors () {
			error = false;

			if ( GetComponent<Rigidbody>() == null ) { Debug.Log( this + ": Projectile must have a Rigidbody on the root level." ); error = true; }

			return error;
		}*/
	}
//}



