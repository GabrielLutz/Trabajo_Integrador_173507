[**portal-dgc-frontend**](../../../../README.md)

***

[portal-dgc-frontend](../../../../README.md) / [core/services/tribunal.service](../README.md) / TribunalService

# Class: TribunalService

Defined in: [src/app/core/services/tribunal.service.ts:22](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/tribunal.service.ts#L22)

## Constructors

### Constructor

> **new TribunalService**(`http`): `TribunalService`

Defined in: [src/app/core/services/tribunal.service.ts:25](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/tribunal.service.ts#L25)

#### Parameters

##### http

`HttpClient`

#### Returns

`TribunalService`

## Methods

### calificarPrueba()

> **calificarPrueba**(`dto`): `Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<[`EvaluacionPrueba`](../../../models/tribunal.models/interfaces/EvaluacionPrueba.md)\>\>

Defined in: [src/app/core/services/tribunal.service.ts:81](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/tribunal.service.ts#L81)

#### Parameters

##### dto

[`CalificarPruebaDto`](../../../models/tribunal.models/interfaces/CalificarPruebaDto.md)

Datos de la prueba a calificar, incluyendo puntaje obtenido y observaciones.

#### Returns

`Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<[`EvaluacionPrueba`](../../../models/tribunal.models/interfaces/EvaluacionPrueba.md)\>\>

Observable con la evaluación de la prueba registrada.

#### Description

RF-11: Registra la evaluación de una prueba asignando un puntaje entre 0 y el máximo permitido.

#### Throws

Cuando la calificación no puede guardarse por errores de negocio o de red.

***

### generarOrdenamiento()

> **generarOrdenamiento**(`dto`): `Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<`any`\>\>

Defined in: [src/app/core/services/tribunal.service.ts:115](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/tribunal.service.ts#L115)

#### Parameters

##### dto

[`GenerarOrdenamientoDto`](../../../models/tribunal.models/interfaces/GenerarOrdenamientoDto.md)

Parámetros de generación del ordenamiento (llamado, puntaje mínimo, cuotas y banderas de definitividad).

#### Returns

`Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<`any`\>\>

Observable con el resultado de la generación del ordenamiento y estadísticas asociadas.

#### Description

RF-14: Genera listas de ordenamiento aplicando reglas de desempate y sorteo aleatorio cuando corresponde.

#### Throws

Cuando se produce un error al generar las listas de prelación.

***

### obtenerDetalleEvaluacion()

> **obtenerDetalleEvaluacion**(`inscripcionId`): `Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<[`DetalleEvaluacion`](../../../models/tribunal.models/interfaces/DetalleEvaluacion.md)\>\>

Defined in: [src/app/core/services/tribunal.service.ts:45](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/tribunal.service.ts#L45)

#### Parameters

##### inscripcionId

`number`

Identificador de la inscripción a consultar.

#### Returns

`Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<[`DetalleEvaluacion`](../../../models/tribunal.models/interfaces/DetalleEvaluacion.md)\>\>

Observable con la información consolidada para el tribunal.

#### Description

RF-11: Devuelve el detalle completo de una inscripción, incluyendo evaluaciones, requisitos y méritos.

#### Throws

Cuando el detalle no puede obtenerse.

***

### obtenerDetalleOrdenamiento()

> **obtenerDetalleOrdenamiento**(`ordenamientoId`): `Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<[`OrdenamientoDetalle`](../../../models/tribunal.models/interfaces/OrdenamientoDetalle.md)\>\>

Defined in: [src/app/core/services/tribunal.service.ts:137](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/tribunal.service.ts#L137)

#### Parameters

##### ordenamientoId

`number`

Identificador del ordenamiento requerido.

#### Returns

`Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<[`OrdenamientoDetalle`](../../../models/tribunal.models/interfaces/OrdenamientoDetalle.md)\>\>

Observable con las posiciones y datos adicionales.

#### Description

RF-15: Obtiene el detalle de un ordenamiento específico con las posiciones resultantes.

#### Throws

Cuando el detalle de ordenamiento no está disponible.

***

### obtenerEstadisticas()

> **obtenerEstadisticas**(`llamadoId`): `Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<[`EstadisticasTribunal`](../../../models/tribunal.models/interfaces/EstadisticasTribunal.md)\>\>

Defined in: [src/app/core/services/tribunal.service.ts:57](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/tribunal.service.ts#L57)

#### Parameters

##### llamadoId

`number`

Identificador del llamado.

#### Returns

`Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<[`EstadisticasTribunal`](../../../models/tribunal.models/interfaces/EstadisticasTribunal.md)\>\>

Observable con indicadores de evaluaciones, aprobaciones y cuotas.

#### Description

RF-11: Obtiene estadísticas agregadas del tribunal para un llamado determinado.

#### Throws

Cuando la consulta de estadísticas falla.

***

### obtenerInscripcionesParaEvaluar()

> **obtenerInscripcionesParaEvaluar**(`llamadoId`): `Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<[`InscripcionParaEvaluar`](../../../models/tribunal.models/interfaces/InscripcionParaEvaluar.md)[]\>\>

Defined in: [src/app/core/services/tribunal.service.ts:33](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/tribunal.service.ts#L33)

#### Parameters

##### llamadoId

`number`

Identificador del llamado que se está evaluando.

#### Returns

`Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<[`InscripcionParaEvaluar`](../../../models/tribunal.models/interfaces/InscripcionParaEvaluar.md)[]\>\>

Observable con la colección de inscripciones y sus indicadores de evaluación.

#### Description

RF-11: Recupera las inscripciones de un llamado junto con su estado de evaluación para que el tribunal gestione el proceso.

#### Throws

Cuando ocurre un fallo en la consulta al servicio de tribunal.

***

### obtenerOrdenamientos()

> **obtenerOrdenamientos**(`llamadoId`): `Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<[`Ordenamiento`](../../../models/tribunal.models/interfaces/Ordenamiento.md)[]\>\>

Defined in: [src/app/core/services/tribunal.service.ts:125](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/tribunal.service.ts#L125)

#### Parameters

##### llamadoId

`number`

Identificador del llamado del que se requieren los ordenamientos.

#### Returns

`Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<[`Ordenamiento`](../../../models/tribunal.models/interfaces/Ordenamiento.md)[]\>\>

Observable con la colección de ordenamientos disponibles.

#### Description

RF-15: Obtiene las listas de prelación generadas para un llamado determinado.

#### Throws

Cuando la consulta de ordenamientos falla.

***

### obtenerPruebasDelLlamado()

> **obtenerPruebasDelLlamado**(`llamadoId`): `Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<[`PruebaDto`](../../../models/tribunal.models/interfaces/PruebaDto.md)[]\>\>

Defined in: [src/app/core/services/tribunal.service.ts:69](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/tribunal.service.ts#L69)

#### Parameters

##### llamadoId

`number`

Identificador del llamado.

#### Returns

`Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<[`PruebaDto`](../../../models/tribunal.models/interfaces/PruebaDto.md)[]\>\>

Observable con los metadatos de las pruebas.

#### Description

RF-11: Lista las pruebas configuradas para un llamado con información de evaluaciones registradas.

#### Throws

Cuando la consulta no puede realizarse correctamente.

***

### publicarOrdenamiento()

> **publicarOrdenamiento**(`ordenamientoId`): `Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<`boolean`\>\>

Defined in: [src/app/core/services/tribunal.service.ts:149](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/tribunal.service.ts#L149)

#### Parameters

##### ordenamientoId

`number`

Identificador del ordenamiento a publicar.

#### Returns

`Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<`boolean`\>\>

Observable que indica si la publicación fue exitosa.

#### Description

RF-15: Publica un ordenamiento para dejarlo disponible a otros actores.

#### Throws

Cuando el backend no puede publicar el ordenamiento.

***

### valorarMerito()

> **valorarMerito**(`dto`): `Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<`any`\>\>

Defined in: [src/app/core/services/tribunal.service.ts:91](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/tribunal.service.ts#L91)

#### Parameters

##### dto

[`ValorarMeritoDto`](../../../models/tribunal.models/interfaces/ValorarMeritoDto.md)

Información del mérito del postulante junto con la documentación a validar.

#### Returns

`Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<`any`\>\>

Observable con la evaluación del mérito procesada por el tribunal.

#### Description

RF-12: Valora un mérito individual verificando documentación y asignando puntaje según la tabla de evaluación.

#### Throws

Cuando la valoración falla o la API devuelve un error.

***

### valorarMeritos()

> **valorarMeritos**(`inscripcionId`, `meritos`): `Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<`any`[]\>\>

Defined in: [src/app/core/services/tribunal.service.ts:102](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/core/services/tribunal.service.ts#L102)

#### Parameters

##### inscripcionId

`number`

Identificador de la inscripción evaluada.

##### meritos

[`ValorarMeritoDto`](../../../models/tribunal.models/interfaces/ValorarMeritoDto.md)[]

Listado de méritos a valorar con puntajes y verificaciones.

#### Returns

`Observable`\<[`ApiResponse`](../../../models/tribunal.models/interfaces/ApiResponse.md)\<`any`[]\>\>

Observable con las evaluaciones generadas.

#### Description

RF-12: Valora en lote múltiples méritos asociados a una inscripción.

#### Throws

Cuando ocurre un error durante la valoración masiva.
