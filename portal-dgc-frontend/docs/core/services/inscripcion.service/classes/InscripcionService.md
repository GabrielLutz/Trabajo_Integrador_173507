[**portal-dgc-frontend**](../../../../README.md)

***

[portal-dgc-frontend](../../../../README.md) / [core/services/inscripcion.service](../README.md) / InscripcionService

# Class: InscripcionService

Defined in: [src/app/core/services/inscripcion.service.ts:14](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/inscripcion.service.ts#L14)

## Constructors

### Constructor

> **new InscripcionService**(`apiService`): `InscripcionService`

Defined in: [src/app/core/services/inscripcion.service.ts:17](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/inscripcion.service.ts#L17)

#### Parameters

##### apiService

[`ApiService`](../../api.service/classes/ApiService.md)

#### Returns

`InscripcionService`

## Methods

### crearInscripcion()

> **crearInscripcion**(`postulanteId`, `inscripcion`): `Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<[`InscripcionResponse`](../../../models/inscripcion.model/interfaces/InscripcionResponse.md)\>\>

Defined in: [src/app/core/services/inscripcion.service.ts:26](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/inscripcion.service.ts#L26)

#### Parameters

##### postulanteId

`number`

Identificador del postulante que se está inscribiendo al llamado.

##### inscripcion

[`CrearInscripcion`](../../../models/inscripcion.model/interfaces/CrearInscripcion.md)

Payload con la información necesaria para registrar la inscripción.

#### Returns

`Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<[`InscripcionResponse`](../../../models/inscripcion.model/interfaces/InscripcionResponse.md)\>\>

Observable con la respuesta del API que incluye los datos de la inscripción creada.

#### Description

RF-05: Crea una inscripción completa que incluye autodefinición, requisitos, méritos y apoyos solicitados.

#### Throws

Cuando la creación falla por validaciones de negocio o errores de comunicación.

***

### obtenerInscripcion()

> **obtenerInscripcion**(`id`): `Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<[`InscripcionResponse`](../../../models/inscripcion.model/interfaces/InscripcionResponse.md)\>\>

Defined in: [src/app/core/services/inscripcion.service.ts:42](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/inscripcion.service.ts#L42)

#### Parameters

##### id

`number`

Identificador de la inscripción que se desea consultar.

#### Returns

`Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<[`InscripcionResponse`](../../../models/inscripcion.model/interfaces/InscripcionResponse.md)\>\>

Observable con la respuesta del API y la inscripción detallada.

#### Description

RF-08: Obtiene el detalle completo de una inscripción, incluyendo requisitos, méritos y apoyos registrados.

#### Throws

Cuando la inscripción no existe o ocurre un error al consultar el backend.

***

### obtenerInscripcionesPorPostulante()

> **obtenerInscripcionesPorPostulante**(`postulanteId`): `Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<[`InscripcionSimple`](../../../models/inscripcion.model/interfaces/InscripcionSimple.md)[]\>\>

Defined in: [src/app/core/services/inscripcion.service.ts:54](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/inscripcion.service.ts#L54)

#### Parameters

##### postulanteId

`number`

Identificador del postulante del que se desean recuperar las inscripciones.

#### Returns

`Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<[`InscripcionSimple`](../../../models/inscripcion.model/interfaces/InscripcionSimple.md)[]\>\>

Observable con la colección de inscripciones resumidas.

#### Description

RF-07: Lista las inscripciones realizadas por un postulante junto con su estado actual.

#### Throws

Cuando ocurre un error durante la consulta al servicio.

***

### validarInscripcionExistente()

> **validarInscripcionExistente**(`postulanteId`, `llamadoId`): `Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<`boolean`\>\>

Defined in: [src/app/core/services/inscripcion.service.ts:69](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/inscripcion.service.ts#L69)

#### Parameters

##### postulanteId

`number`

Identificador del postulante.

##### llamadoId

`number`

Identificador del llamado al que se intenta inscribir.

#### Returns

`Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<`boolean`\>\>

Observable con true si ya existe una inscripción y false si se puede crear una nueva.

#### Description

RF-05: Valida si ya existe una inscripción del postulante para el llamado indicado antes de permitir un nuevo registro.

#### Throws

Cuando el servicio devuelve un error.
