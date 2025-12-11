[**portal-dgc-frontend**](../../../../../../README.md)

***

[portal-dgc-frontend](../../../../../../README.md) / [features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component](../README.md) / FormularioInscripcionComponent

# Class: FormularioInscripcionComponent

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:19](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L19)

## Implements

- `OnInit`
- `OnDestroy`
- `AfterViewInit`

## Constructors

### Constructor

> **new FormularioInscripcionComponent**(`route`, `router`, `inscripcionService`, `llamadoService`, `postulanteService`): `FormularioInscripcionComponent`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:49](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L49)

#### Parameters

##### route

`ActivatedRoute`

##### router

`Router`

##### inscripcionService

[`InscripcionService`](../../../../../../core/services/inscripcion.service/classes/InscripcionService.md)

##### llamadoService

[`LlamadoService`](../../../../../../core/services/llamado.service/classes/LlamadoService.md)

##### postulanteService

[`PostulanteService`](../../../../../../core/services/postulante.service/classes/PostulanteService.md)

#### Returns

`FormularioInscripcionComponent`

## Properties

### aceptaTerminos

> **aceptaTerminos**: `boolean` = `false`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:26](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L26)

***

### error

> **error**: `string` = `''`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:30](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L30)

***

### formApoyos

> `readonly` **formApoyos**: `FormGroup`\<`ApoyosForm`\>

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:45](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L45)

***

### formAutodefinicion

> `readonly` **formAutodefinicion**: `FormGroup`\<`AutodefinicionForm`\>

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:42](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L42)

***

### formDepartamento

> `readonly` **formDepartamento**: `FormGroup`\<`DepartamentoForm`\>

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:41](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L41)

***

### formMeritos

> `readonly` **formMeritos**: `FormGroup`\<`MeritosForm`\>

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:44](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L44)

***

### formRequisitos

> `readonly` **formRequisitos**: `FormGroup`\<`RequisitosForm`\>

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:43](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L43)

***

### llamado

> **llamado**: [`LlamadoDetalle`](../../../../../../core/models/llamado.model/interfaces/LlamadoDetalle.md) \| `null` = `null`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:28](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L28)

***

### llamadoId

> **llamadoId**: `number`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:24](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L24)

***

### loading

> **loading**: `boolean` = `false`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:29](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L29)

***

### pasoActual

> **pasoActual**: `number` = `0`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:23](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L23)

***

### pasos

> `readonly` **pasos**: `object`[]

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:32](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L32)

#### icono

> **icono**: `string` = `'📍'`

#### titulo

> **titulo**: `string` = `'Departamento'`

***

### postulanteId

> **postulanteId**: `number` = `1`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:25](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L25)

## Accessors

### apoyosArray

#### Get Signature

> **get** **apoyosArray**(): `FormArray`\<`FormControl`\<`boolean`\>\>

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:133](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L133)

Arreglo reactivo con los apoyos que el postulante puede solicitar.

##### Returns

`FormArray`\<`FormControl`\<`boolean`\>\>

***

### meritosArray

#### Get Signature

> **get** **meritosArray**(): `FormArray`\<`MeritoFormGroup`\>

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:126](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L126)

Arreglo reactivo con los méritos disponibles para cargar respaldos.

##### Returns

`FormArray`\<`MeritoFormGroup`\>

***

### requisitosArray

#### Get Signature

> **get** **requisitosArray**(): `FormArray`\<`RequisitoFormGroup`\>

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:119](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L119)

Arreglo reactivo con los requisitos del llamado.

##### Returns

`FormArray`\<`RequisitoFormGroup`\>

## Methods

### cancelar()

> **cancelar**(): `void`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:357](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L357)

Cancela la inscripción y regresa al detalle del llamado actual.

#### Returns

`void`

***

### cargarLlamado()

> **cargarLlamado**(): `void`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:140](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L140)

Recupera el detalle del llamado e inicializa cada paso del formulario (RF-04/RF-05).

#### Returns

`void`

***

### contarApoyos()

> **contarApoyos**(): `number`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:350](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L350)

Cantidad de apoyos solicitados en el paso correspondiente.

#### Returns

`number`

***

### contarMeritos()

> **contarMeritos**(): `number`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:341](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L341)

Cantidad de méritos seleccionados para enviar al backend.

#### Returns

`number`

***

### contarRequisitos()

> **contarRequisitos**(): `number`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:332](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L332)

Cantidad de requisitos marcados como cumplidos por el postulante.

#### Returns

`number`

***

### enviarInscripcion()

> **enviarInscripcion**(): `void`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:267](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L267)

Construye el payload y lo envía al backend validando términos y campos requeridos (RF-05).

#### Returns

`void`

***

### irAPaso()

> **irAPaso**(`paso`): `void`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:233](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L233)

Permite navegar a un paso ya completado para revisar información.

#### Parameters

##### paso

`number`

#### Returns

`void`

***

### ngAfterViewInit()

> **ngAfterViewInit**(): `void`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:102](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L102)

Registra listeners de archivos una vez que Angular renderiza los inputs de méritos.

#### Returns

`void`

#### Implementation of

`AfterViewInit.ngAfterViewInit`

***

### ngOnDestroy()

> **ngOnDestroy**(): `void`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:111](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L111)

Libera recursos asociados al formulario cuando se destruye el componente.

#### Returns

`void`

#### Implementation of

`OnDestroy.ngOnDestroy`

***

### ngOnInit()

> **ngOnInit**(): `void`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:84](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L84)

Obtiene parámetros iniciales y dispara la carga del llamado y las validaciones (RF-05).

#### Returns

`void`

#### Implementation of

`OnInit.ngOnInit`

***

### obtenerNombreDepartamento()

> **obtenerNombreDepartamento**(): `string`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:316](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L316)

Devuelve el nombre del departamento seleccionado para mostrar en la UI.

#### Returns

`string`

***

### pasoAnterior()

> **pasoAnterior**(): `void`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:224](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L224)

Retrocede al paso inmediato anterior.

#### Returns

`void`

***

### prepararFormularios()

> **prepararFormularios**(): `void`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:176](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L176)

Inicializa los formularios dinámicos de requisitos, méritos y apoyos.

#### Returns

`void`

***

### siguientePaso()

> **siguientePaso**(): `void`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:201](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L201)

Avanza al siguiente paso del flujo guiado validando el formulario actual.

#### Returns

`void`

***

### validarPostulante()

> **validarPostulante**(): `void`

Defined in: [src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts:162](https://github.com/GabrielLutz/Trabajo_Integrador_173507/blob/781901187ed30c0948ea7a73f9b130dfded3d489/portal-dgc-frontend/src/app/features/inscripcion/components/formulario-inscripcion/formulario-inscripcion.component.ts#L162)

Verifica que el postulante tenga sus datos completos antes de continuar (RF-02).

#### Returns

`void`
