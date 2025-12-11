[**portal-dgc-frontend**](../../../../README.md)

***

[portal-dgc-frontend](../../../../README.md) / [core/services/postulante.service](../README.md) / PostulanteService

# Class: PostulanteService

Defined in: [src/app/core/services/postulante.service.ts:10](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/postulante.service.ts#L10)

## Constructors

### Constructor

> **new PostulanteService**(`apiService`): `PostulanteService`

Defined in: [src/app/core/services/postulante.service.ts:13](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/postulante.service.ts#L13)

#### Parameters

##### apiService

[`ApiService`](../../api.service/classes/ApiService.md)

#### Returns

`PostulanteService`

## Methods

### actualizarDatosPersonales()

> **actualizarDatosPersonales**(`id`, `datos`): `Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<[`Postulante`](../../../models/postulante.model/interfaces/Postulante.md)\>\>

Defined in: [src/app/core/services/postulante.service.ts:32](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/postulante.service.ts#L32)

#### Parameters

##### id

`number`

Identificador del postulante a actualizar.

##### datos

[`PostulanteDatosPersonales`](../../../models/postulante.model/interfaces/PostulanteDatosPersonales.md)

Datos personales completos que se desean persistir.

#### Returns

`Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<[`Postulante`](../../../models/postulante.model/interfaces/Postulante.md)\>\>

Observable con la respuesta del API y el postulante actualizado cuando la operación es exitosa.

#### Description

RF-02: Actualiza la información personal del postulante aplicando las validaciones de negocio.

#### Throws

Cuando el backend rechaza la actualización o se produce un error de comunicación.

***

### obtenerPostulante()

> **obtenerPostulante**(`id`): `Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<[`Postulante`](../../../models/postulante.model/interfaces/Postulante.md)\>\>

Defined in: [src/app/core/services/postulante.service.ts:21](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/postulante.service.ts#L21)

#### Parameters

##### id

`number`

Identificador único del postulante cuyos datos se desean visualizar.

#### Returns

`Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<[`Postulante`](../../../models/postulante.model/interfaces/Postulante.md)\>\>

Observable con la respuesta del API que incluye la información del postulante y mensajes de negocio.

#### Description

RF-01: Obtiene los datos del postulante y el indicador de completitud del perfil.

#### Throws

Cuando el backend responde con error de red u otro estado diferente de 2xx.

***

### validarCedulaDisponible()

> **validarCedulaDisponible**(`cedula`): `Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<`boolean`\>\>

Defined in: [src/app/core/services/postulante.service.ts:48](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/postulante.service.ts#L48)

#### Parameters

##### cedula

`string`

Número de cédula uruguaya sin puntos ni guiones que se desea validar.

#### Returns

`Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<`boolean`\>\>

Observable con true cuando la cédula está disponible y false cuando ya existe.

#### Description

RF-20: Verifica en tiempo real la disponibilidad de una cédula de identidad.

#### Throws

Cuando ocurre un error al consultar el servicio de validación.

***

### validarEmailDisponible()

> **validarEmailDisponible**(`email`): `Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<`boolean`\>\>

Defined in: [src/app/core/services/postulante.service.ts:60](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/postulante.service.ts#L60)

#### Parameters

##### email

`string`

Dirección de correo electrónico a validar.

#### Returns

`Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<`boolean`\>\>

Observable con true cuando el email está disponible, false en caso contrario.

#### Description

RF-20: Comprueba si un email está libre para ser utilizado por un postulante.

#### Throws

Cuando se produce un error al contactar el servicio de validación.
