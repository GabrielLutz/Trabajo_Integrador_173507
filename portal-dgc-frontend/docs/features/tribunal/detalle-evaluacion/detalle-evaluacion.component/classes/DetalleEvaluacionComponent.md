[**portal-dgc-frontend**](../../../../../README.md)

***

[portal-dgc-frontend](../../../../../README.md) / [features/tribunal/detalle-evaluacion/detalle-evaluacion.component](../README.md) / DetalleEvaluacionComponent

# Class: DetalleEvaluacionComponent

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:16](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L16)

## Implements

- `OnInit`

## Constructors

### Constructor

> **new DetalleEvaluacionComponent**(`route`, `router`, `fb`, `tribunalService`): `DetalleEvaluacionComponent`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:30](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L30)

#### Parameters

##### route

`ActivatedRoute`

##### router

`Router`

##### fb

`FormBuilder`

##### tribunalService

[`TribunalService`](../../../../../core/services/tribunal.service/classes/TribunalService.md)

#### Returns

`DetalleEvaluacionComponent`

## Properties

### detalle

> **detalle**: [`DetalleEvaluacion`](../../../../../core/models/tribunal.models/interfaces/DetalleEvaluacion.md) \| `null` = `null`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:17](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L17)

***

### error

> **error**: `string` \| `null` = `null`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:20](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L20)

***

### formCalificarPrueba

> **formCalificarPrueba**: `FormGroup`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:26](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L26)

***

### formValorarMeritos

> **formValorarMeritos**: `FormGroup`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:27](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L27)

***

### guardandoMeritos

> **guardandoMeritos**: `boolean` = `false`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:23](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L23)

***

### guardandoPrueba

> **guardandoPrueba**: `boolean` = `false`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:22](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L22)

***

### inscripcionId

> **inscripcionId**: `number`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:24](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L24)

***

### loading

> **loading**: `boolean` = `false`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:19](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L19)

***

### mensajeExito

> **mensajeExito**: `string` \| `null` = `null`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:21](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L21)

***

### pruebas

> **pruebas**: [`PruebaDto`](../../../../../core/models/tribunal.models/interfaces/PruebaDto.md)[] = `[]`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:18](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L18)

***

### pruebaSeleccionada

> **pruebaSeleccionada**: [`PruebaDto`](../../../../../core/models/tribunal.models/interfaces/PruebaDto.md) \| `null` = `null`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:28](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L28)

***

### seccionActiva

> **seccionActiva**: `"requisitos"` \| `"meritos"` \| `"pruebas"` = `'pruebas'`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:25](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L25)

## Methods

### calificarPrueba()

> **calificarPrueba**(): `void`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:152](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L152)

Envía al backend la calificación de la prueba elegida (RF-11).

#### Returns

`void`

***

### cambiarSeccion()

> **cambiarSeccion**(`seccion`): `void`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:226](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L226)

Cambia la sección visible del detalle según la acción del tribunal.

#### Parameters

##### seccion

`"requisitos"` | `"meritos"` | `"pruebas"`

#### Returns

`void`

***

### cargarDetalle()

> **cargarDetalle**(): `void`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:57](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L57)

Recupera la información completa para evaluar requisitos, pruebas y méritos.

#### Returns

`void`

***

### cargarPruebasDisponibles()

> **cargarPruebasDisponibles**(): `void`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:83](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L83)

Obtiene las pruebas disponibles del llamado para vincularlas a la evaluación.

#### Returns

`void`

***

### estaPruebaEvaluada()

> **estaPruebaEvaluada**(`pruebaId`): `boolean`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:241](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L241)

Indica si la prueba ya fue calificada para reflejarlo en la UI.

#### Parameters

##### pruebaId

`number`

#### Returns

`boolean`

***

### getPuntajeMaximoMerito()

> **getPuntajeMaximoMerito**(`meritoId`): `number`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:233](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L233)

Devuelve el puntaje máximo configurado para un mérito específico.

#### Parameters

##### meritoId

`number`

#### Returns

`number`

***

### inicializarFormularioMeritos()

> **inicializarFormularioMeritos**(): `void`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:103](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L103)

Construye dinámicamente el formulario de méritos con validaciones por item (RF-12).

#### Returns

`void`

***

### ngOnInit()

> **ngOnInit**(): `void`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:47](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L47)

Obtiene la inscripción a evaluar y carga su detalle (RF-11/RF-12).

#### Returns

`void`

#### Implementation of

`OnInit.ngOnInit`

***

### seleccionarPrueba()

> **seleccionarPrueba**(`prueba`): `void`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:125](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L125)

Selecciona una prueba a calificar y precarga sus datos si ya fue evaluada.

#### Parameters

##### prueba

[`PruebaDto`](../../../../../core/models/tribunal.models/interfaces/PruebaDto.md)

#### Returns

`void`

***

### valorarTodosMeritos()

> **valorarTodosMeritos**(): `void`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:190](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L190)

Persiste en lote las valoraciones de todos los méritos del postulante (RF-12).

#### Returns

`void`

***

### volver()

> **volver**(): `void`

Defined in: [src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts:248](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/tribunal/detalle-evaluacion/detalle-evaluacion.component.ts#L248)

Vuelve a la lista de inscripciones pendientes del tribunal.

#### Returns

`void`
