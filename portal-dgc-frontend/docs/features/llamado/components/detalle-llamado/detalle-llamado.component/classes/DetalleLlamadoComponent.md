[**portal-dgc-frontend**](../../../../../../README.md)

***

[portal-dgc-frontend](../../../../../../README.md) / [features/llamado/components/detalle-llamado/detalle-llamado.component](../README.md) / DetalleLlamadoComponent

# Class: DetalleLlamadoComponent

Defined in: [src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts:13](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts#L13)

## Implements

- `OnInit`

## Constructors

### Constructor

> **new DetalleLlamadoComponent**(`route`, `router`, `llamadoService`): `DetalleLlamadoComponent`

Defined in: [src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts:19](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts#L19)

#### Parameters

##### route

`ActivatedRoute`

##### router

`Router`

##### llamadoService

[`LlamadoService`](../../../../../../core/services/llamado.service/classes/LlamadoService.md)

#### Returns

`DetalleLlamadoComponent`

## Properties

### error

> **error**: `string` = `''`

Defined in: [src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts:16](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts#L16)

***

### llamado

> **llamado**: [`LlamadoDetalle`](../../../../../../core/models/llamado.model/interfaces/LlamadoDetalle.md) \| `null` = `null`

Defined in: [src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts:14](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts#L14)

***

### loading

> **loading**: `boolean` = `false`

Defined in: [src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts:15](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts#L15)

***

### tabActiva

> **tabActiva**: `"informacion"` \| `"requisitos"` \| `"puntuables"` \| `"apoyos"` = `'informacion'`

Defined in: [src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts:17](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts#L17)

## Accessors

### puedeInscribirse

#### Get Signature

> **get** **puedeInscribirse**(): `boolean`

Defined in: [src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts:82](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts#L82)

Valida si el postulante puede acceder al flujo de inscripción.

##### Returns

`boolean`

## Methods

### cambiarTab()

> **cambiarTab**(`tab`): `void`

Defined in: [src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts:57](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts#L57)

Cambia la pestaña visible en el detalle del llamado.

#### Parameters

##### tab

`"informacion"` | `"requisitos"` | `"puntuables"` | `"apoyos"`

#### Returns

`void`

***

### cargarLlamado()

> **cargarLlamado**(`id`): `void`

Defined in: [src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts:38](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts#L38)

Pide al backend la información completa del llamado.

#### Parameters

##### id

`number`

#### Returns

`void`

***

### getEstadoBadgeClass()

> **getEstadoBadgeClass**(): `string`

Defined in: [src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts:97](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts#L97)

Determina la clase visual del badge según el estado del llamado.

#### Returns

`string`

***

### inscribirse()

> **inscribirse**(): `void`

Defined in: [src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts:64](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts#L64)

Inicia el proceso de inscripción cuando el llamado está habilitado (RF-05).

#### Returns

`void`

***

### ngOnInit()

> **ngOnInit**(): `void`

Defined in: [src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts:28](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts#L28)

Obtiene el identificador de ruta y carga el detalle del llamado (RF-04).

#### Returns

`void`

#### Implementation of

`OnInit.ngOnInit`

***

### volver()

> **volver**(): `void`

Defined in: [src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts:75](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/llamado/components/detalle-llamado/detalle-llamado.component.ts#L75)

Regresa al listado general de llamados.

#### Returns

`void`
