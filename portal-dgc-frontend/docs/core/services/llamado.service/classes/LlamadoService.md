[**portal-dgc-frontend**](../../../../README.md)

***

[portal-dgc-frontend](../../../../README.md) / [core/services/llamado.service](../README.md) / LlamadoService

# Class: LlamadoService

Defined in: [src/app/core/services/llamado.service.ts:10](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/llamado.service.ts#L10)

## Constructors

### Constructor

> **new LlamadoService**(`apiService`): `LlamadoService`

Defined in: [src/app/core/services/llamado.service.ts:13](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/llamado.service.ts#L13)

#### Parameters

##### apiService

[`ApiService`](../../api.service/classes/ApiService.md)

#### Returns

`LlamadoService`

## Methods

### obtenerLlamadoDetalle()

> **obtenerLlamadoDetalle**(`id`): `Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<[`LlamadoDetalle`](../../../models/llamado.model/interfaces/LlamadoDetalle.md)\>\>

Defined in: [src/app/core/services/llamado.service.ts:39](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/llamado.service.ts#L39)

#### Parameters

##### id

`number`

Identificador del llamado que se desea consultar.

#### Returns

`Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<[`LlamadoDetalle`](../../../models/llamado.model/interfaces/LlamadoDetalle.md)\>\>

Observable con la respuesta del API y el detalle del llamado solicitado.

#### Description

RF-04: Obtiene el detalle completo de un llamado incluyendo requisitos excluyentes, ítems puntuables y apoyos disponibles.

#### Throws

Cuando el llamado no existe o la consulta genera un error.

***

### obtenerLlamadosActivos()

> **obtenerLlamadosActivos**(): `Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<[`Llamado`](../../../models/llamado.model/interfaces/Llamado.md)[]\>\>

Defined in: [src/app/core/services/llamado.service.ts:20](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/llamado.service.ts#L20)

#### Returns

`Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<[`Llamado`](../../../models/llamado.model/interfaces/Llamado.md)[]\>\>

Observable con la respuesta del API que contiene los llamados activos.

#### Description

RF-03: Recupera la lista de llamados vigentes disponibles para inscripción, respetando filtros definidos en el backend.

#### Throws

Cuando se produce un error al consultar los llamados vigentes.

***

### obtenerLlamadosInactivos()

> **obtenerLlamadosInactivos**(): `Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<[`Llamado`](../../../models/llamado.model/interfaces/Llamado.md)[]\>\>

Defined in: [src/app/core/services/llamado.service.ts:29](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/llamado.service.ts#L29)

#### Returns

`Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<[`Llamado`](../../../models/llamado.model/interfaces/Llamado.md)[]\>\>

Observable con los llamados inactivos registrados por el sistema.

#### Description

RF-03: Recupera los llamados que ya no están vigentes para tareas administrativas o de seguimiento.

#### Throws

Cuando la consulta de llamados inactivos falla.

***

### validarLlamadoDisponible()

> **validarLlamadoDisponible**(`id`): `Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<`boolean`\>\>

Defined in: [src/app/core/services/llamado.service.ts:49](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/llamado.service.ts#L49)

#### Parameters

##### id

`number`

Identificador del llamado a validar.

#### Returns

`Observable`\<[`ApiResponse`](../../../models/api-response.model/interfaces/ApiResponse.md)\<`boolean`\>\>

Observable con true cuando el llamado está disponible y false en caso contrario.

#### Description

RF-03: Valida si el llamado sigue abierto para nuevas inscripciones de postulantes.

#### Throws

Cuando no se puede contactar el servicio de llamados.
