	.arch	armv7-a
	.syntax unified
	.eabi_attribute 67, "2.09"	@ Tag_conformance
	.eabi_attribute 6, 10	@ Tag_CPU_arch
	.eabi_attribute 7, 65	@ Tag_CPU_arch_profile
	.eabi_attribute 8, 1	@ Tag_ARM_ISA_use
	.eabi_attribute 9, 2	@ Tag_THUMB_ISA_use
	.fpu	vfpv3-d16
	.eabi_attribute 34, 1	@ Tag_CPU_unaligned_access
	.eabi_attribute 15, 1	@ Tag_ABI_PCS_RW_data
	.eabi_attribute 16, 1	@ Tag_ABI_PCS_RO_data
	.eabi_attribute 17, 2	@ Tag_ABI_PCS_GOT_use
	.eabi_attribute 20, 2	@ Tag_ABI_FP_denormal
	.eabi_attribute 21, 0	@ Tag_ABI_FP_exceptions
	.eabi_attribute 23, 3	@ Tag_ABI_FP_number_model
	.eabi_attribute 24, 1	@ Tag_ABI_align_needed
	.eabi_attribute 25, 1	@ Tag_ABI_align_preserved
	.eabi_attribute 38, 1	@ Tag_ABI_FP_16bit_format
	.eabi_attribute 18, 4	@ Tag_ABI_PCS_wchar_t
	.eabi_attribute 26, 2	@ Tag_ABI_enum_size
	.eabi_attribute 14, 0	@ Tag_ABI_PCS_R9_use
	.file	"typemaps.armeabi-v7a.s"

/* map_module_count: START */
	.section	.rodata.map_module_count,"a",%progbits
	.type	map_module_count, %object
	.p2align	2
	.global	map_module_count
map_module_count:
	.size	map_module_count, 4
	.long	2
/* map_module_count: END */

/* java_type_count: START */
	.section	.rodata.java_type_count,"a",%progbits
	.type	java_type_count, %object
	.p2align	2
	.global	java_type_count
java_type_count:
	.size	java_type_count, 4
	.long	198
/* java_type_count: END */

	.include	"typemaps.armeabi-v7a-shared.inc"
	.include	"typemaps.armeabi-v7a-managed.inc"

/* Managed to Java map: START */
	.section	.data.rel.map_modules,"aw",%progbits
	.type	map_modules, %object
	.p2align	2
	.global	map_modules
map_modules:
	/* module_uuid: 7458a11a-c25d-498c-9e6b-23c76d57654b */
	.byte	0x1a, 0xa1, 0x58, 0x74, 0x5d, 0xc2, 0x8c, 0x49, 0x9e, 0x6b, 0x23, 0xc7, 0x6d, 0x57, 0x65, 0x4b
	/* entry_count */
	.long	6
	/* duplicate_count */
	.long	0
	/* map */
	.long	module0_managed_to_java
	/* duplicate_map */
	.long	0
	/* assembly_name: AppBibliaNVI */
	.long	.L.map_aname.0
	/* image */
	.long	0
	/* java_name_width */
	.long	0
	/* java_map */
	.long	0

	/* module_uuid: 0753f2eb-fc91-4a9b-9c7b-fae813040801 */
	.byte	0xeb, 0xf2, 0x53, 0x07, 0x91, 0xfc, 0x9b, 0x4a, 0x9c, 0x7b, 0xfa, 0xe8, 0x13, 0x04, 0x08, 0x01
	/* entry_count */
	.long	192
	/* duplicate_count */
	.long	34
	/* map */
	.long	module1_managed_to_java
	/* duplicate_map */
	.long	module1_managed_to_java_duplicates
	/* assembly_name: Mono.Android */
	.long	.L.map_aname.1
	/* image */
	.long	0
	/* java_name_width */
	.long	0
	/* java_map */
	.long	0

	.size	map_modules, 96
/* Managed to Java map: END */

/* Java to managed map: START */
	.section	.rodata.map_java,"a",%progbits
	.type	map_java, %object
	.p2align	2
	.global	map_java
map_java:
	/* #0 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554701
	/* java_name */
	.ascii	"android/app/Activity"
	.zero	46
	.zero	2

	/* #1 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554702
	/* java_name */
	.ascii	"android/app/Application"
	.zero	43
	.zero	2

	/* #2 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554713
	/* java_name */
	.ascii	"android/content/ComponentCallbacks"
	.zero	32
	.zero	2

	/* #3 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554715
	/* java_name */
	.ascii	"android/content/ComponentCallbacks2"
	.zero	31
	.zero	2

	/* #4 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554709
	/* java_name */
	.ascii	"android/content/ComponentName"
	.zero	37
	.zero	2

	/* #5 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554707
	/* java_name */
	.ascii	"android/content/Context"
	.zero	43
	.zero	2

	/* #6 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554711
	/* java_name */
	.ascii	"android/content/ContextWrapper"
	.zero	36
	.zero	2

	/* #7 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554719
	/* java_name */
	.ascii	"android/content/DialogInterface"
	.zero	35
	.zero	2

	/* #8 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554717
	/* java_name */
	.ascii	"android/content/DialogInterface$OnClickListener"
	.zero	19
	.zero	2

	/* #9 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554708
	/* java_name */
	.ascii	"android/content/Intent"
	.zero	44
	.zero	2

	/* #10 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554723
	/* java_name */
	.ascii	"android/content/res/AssetManager"
	.zero	34
	.zero	2

	/* #11 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554724
	/* java_name */
	.ascii	"android/content/res/Configuration"
	.zero	33
	.zero	2

	/* #12 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554726
	/* java_name */
	.ascii	"android/content/res/Resources"
	.zero	37
	.zero	2

	/* #13 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554727
	/* java_name */
	.ascii	"android/content/res/Resources$Theme"
	.zero	31
	.zero	2

	/* #14 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554725
	/* java_name */
	.ascii	"android/content/res/XmlResourceParser"
	.zero	29
	.zero	2

	/* #15 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554532
	/* java_name */
	.ascii	"android/database/DataSetObserver"
	.zero	34
	.zero	2

	/* #16 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554694
	/* java_name */
	.ascii	"android/graphics/Point"
	.zero	44
	.zero	2

	/* #17 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554695
	/* java_name */
	.ascii	"android/graphics/Rect"
	.zero	45
	.zero	2

	/* #18 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554696
	/* java_name */
	.ascii	"android/graphics/drawable/Drawable"
	.zero	32
	.zero	2

	/* #19 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554698
	/* java_name */
	.ascii	"android/graphics/drawable/Drawable$Callback"
	.zero	23
	.zero	2

	/* #20 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554684
	/* java_name */
	.ascii	"android/os/BaseBundle"
	.zero	45
	.zero	2

	/* #21 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554685
	/* java_name */
	.ascii	"android/os/Bundle"
	.zero	49
	.zero	2

	/* #22 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554683
	/* java_name */
	.ascii	"android/os/Handler"
	.zero	48
	.zero	2

	/* #23 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554688
	/* java_name */
	.ascii	"android/os/Looper"
	.zero	49
	.zero	2

	/* #24 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554689
	/* java_name */
	.ascii	"android/os/Parcel"
	.zero	49
	.zero	2

	/* #25 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554687
	/* java_name */
	.ascii	"android/os/Parcelable"
	.zero	45
	.zero	2

	/* #26 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554773
	/* java_name */
	.ascii	"android/runtime/JavaProxyThrowable"
	.zero	32
	.zero	2

	/* #27 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554801
	/* java_name */
	.ascii	"android/runtime/XmlReaderPullParser"
	.zero	31
	.zero	2

	/* #28 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554800
	/* java_name */
	.ascii	"android/runtime/XmlReaderResourceParser"
	.zero	27
	.zero	2

	/* #29 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554665
	/* java_name */
	.ascii	"android/text/Editable"
	.zero	45
	.zero	2

	/* #30 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554668
	/* java_name */
	.ascii	"android/text/GetChars"
	.zero	45
	.zero	2

	/* #31 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554671
	/* java_name */
	.ascii	"android/text/InputFilter"
	.zero	42
	.zero	2

	/* #32 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554673
	/* java_name */
	.ascii	"android/text/NoCopySpan"
	.zero	43
	.zero	2

	/* #33 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554675
	/* java_name */
	.ascii	"android/text/Spannable"
	.zero	44
	.zero	2

	/* #34 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554678
	/* java_name */
	.ascii	"android/text/Spanned"
	.zero	46
	.zero	2

	/* #35 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554681
	/* java_name */
	.ascii	"android/text/TextWatcher"
	.zero	42
	.zero	2

	/* #36 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554663
	/* java_name */
	.ascii	"android/util/AttributeSet"
	.zero	41
	.zero	2

	/* #37 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554661
	/* java_name */
	.ascii	"android/util/DisplayMetrics"
	.zero	39
	.zero	2

	/* #38 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554604
	/* java_name */
	.ascii	"android/view/ActionMode"
	.zero	43
	.zero	2

	/* #39 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554606
	/* java_name */
	.ascii	"android/view/ActionMode$Callback"
	.zero	34
	.zero	2

	/* #40 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554609
	/* java_name */
	.ascii	"android/view/ActionProvider"
	.zero	39
	.zero	2

	/* #41 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554617
	/* java_name */
	.ascii	"android/view/ContextMenu"
	.zero	42
	.zero	2

	/* #42 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554615
	/* java_name */
	.ascii	"android/view/ContextMenu$ContextMenuInfo"
	.zero	26
	.zero	2

	/* #43 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554611
	/* java_name */
	.ascii	"android/view/ContextThemeWrapper"
	.zero	34
	.zero	2

	/* #44 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554612
	/* java_name */
	.ascii	"android/view/Display"
	.zero	46
	.zero	2

	/* #45 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554626
	/* java_name */
	.ascii	"android/view/InputEvent"
	.zero	43
	.zero	2

	/* #46 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554585
	/* java_name */
	.ascii	"android/view/KeyEvent"
	.zero	45
	.zero	2

	/* #47 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554587
	/* java_name */
	.ascii	"android/view/KeyEvent$Callback"
	.zero	36
	.zero	2

	/* #48 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554588
	/* java_name */
	.ascii	"android/view/LayoutInflater"
	.zero	39
	.zero	2

	/* #49 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554590
	/* java_name */
	.ascii	"android/view/LayoutInflater$Factory"
	.zero	31
	.zero	2

	/* #50 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554592
	/* java_name */
	.ascii	"android/view/LayoutInflater$Factory2"
	.zero	30
	.zero	2

	/* #51 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554619
	/* java_name */
	.ascii	"android/view/Menu"
	.zero	49
	.zero	2

	/* #52 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554641
	/* java_name */
	.ascii	"android/view/MenuInflater"
	.zero	41
	.zero	2

	/* #53 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554625
	/* java_name */
	.ascii	"android/view/MenuItem"
	.zero	45
	.zero	2

	/* #54 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554621
	/* java_name */
	.ascii	"android/view/MenuItem$OnActionExpandListener"
	.zero	22
	.zero	2

	/* #55 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554623
	/* java_name */
	.ascii	"android/view/MenuItem$OnMenuItemClickListener"
	.zero	21
	.zero	2

	/* #56 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554593
	/* java_name */
	.ascii	"android/view/MotionEvent"
	.zero	42
	.zero	2

	/* #57 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554644
	/* java_name */
	.ascii	"android/view/SearchEvent"
	.zero	42
	.zero	2

	/* #58 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554629
	/* java_name */
	.ascii	"android/view/SubMenu"
	.zero	46
	.zero	2

	/* #59 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554577
	/* java_name */
	.ascii	"android/view/View"
	.zero	49
	.zero	2

	/* #60 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554579
	/* java_name */
	.ascii	"android/view/View$OnClickListener"
	.zero	33
	.zero	2

	/* #61 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554582
	/* java_name */
	.ascii	"android/view/View$OnCreateContextMenuListener"
	.zero	21
	.zero	2

	/* #62 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554648
	/* java_name */
	.ascii	"android/view/ViewGroup"
	.zero	44
	.zero	2

	/* #63 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554649
	/* java_name */
	.ascii	"android/view/ViewGroup$LayoutParams"
	.zero	31
	.zero	2

	/* #64 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554631
	/* java_name */
	.ascii	"android/view/ViewManager"
	.zero	42
	.zero	2

	/* #65 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554633
	/* java_name */
	.ascii	"android/view/ViewParent"
	.zero	43
	.zero	2

	/* #66 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554594
	/* java_name */
	.ascii	"android/view/ViewTreeObserver"
	.zero	37
	.zero	2

	/* #67 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554596
	/* java_name */
	.ascii	"android/view/ViewTreeObserver$OnGlobalLayoutListener"
	.zero	14
	.zero	2

	/* #68 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554598
	/* java_name */
	.ascii	"android/view/ViewTreeObserver$OnPreDrawListener"
	.zero	19
	.zero	2

	/* #69 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554600
	/* java_name */
	.ascii	"android/view/ViewTreeObserver$OnTouchModeChangeListener"
	.zero	11
	.zero	2

	/* #70 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554601
	/* java_name */
	.ascii	"android/view/Window"
	.zero	47
	.zero	2

	/* #71 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554603
	/* java_name */
	.ascii	"android/view/Window$Callback"
	.zero	38
	.zero	2

	/* #72 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554636
	/* java_name */
	.ascii	"android/view/WindowManager"
	.zero	40
	.zero	2

	/* #73 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554634
	/* java_name */
	.ascii	"android/view/WindowManager$LayoutParams"
	.zero	27
	.zero	2

	/* #74 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554654
	/* java_name */
	.ascii	"android/view/accessibility/AccessibilityEvent"
	.zero	21
	.zero	2

	/* #75 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554660
	/* java_name */
	.ascii	"android/view/accessibility/AccessibilityEventSource"
	.zero	15
	.zero	2

	/* #76 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554655
	/* java_name */
	.ascii	"android/view/accessibility/AccessibilityRecord"
	.zero	20
	.zero	2

	/* #77 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554534
	/* java_name */
	.ascii	"android/widget/AbsListView"
	.zero	40
	.zero	2

	/* #78 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554546
	/* java_name */
	.ascii	"android/widget/AbsSpinner"
	.zero	41
	.zero	2

	/* #79 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554560
	/* java_name */
	.ascii	"android/widget/Adapter"
	.zero	44
	.zero	2

	/* #80 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554536
	/* java_name */
	.ascii	"android/widget/AdapterView"
	.zero	40
	.zero	2

	/* #81 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554538
	/* java_name */
	.ascii	"android/widget/AdapterView$OnItemSelectedListener"
	.zero	17
	.zero	2

	/* #82 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554549
	/* java_name */
	.ascii	"android/widget/ArrayAdapter"
	.zero	39
	.zero	2

	/* #83 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554550
	/* java_name */
	.ascii	"android/widget/BaseAdapter"
	.zero	40
	.zero	2

	/* #84 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554552
	/* java_name */
	.ascii	"android/widget/Button"
	.zero	45
	.zero	2

	/* #85 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554562
	/* java_name */
	.ascii	"android/widget/Checkable"
	.zero	42
	.zero	2

	/* #86 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554553
	/* java_name */
	.ascii	"android/widget/CompoundButton"
	.zero	37
	.zero	2

	/* #87 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554555
	/* java_name */
	.ascii	"android/widget/Filter"
	.zero	45
	.zero	2

	/* #88 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554557
	/* java_name */
	.ascii	"android/widget/Filter$FilterListener"
	.zero	30
	.zero	2

	/* #89 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554564
	/* java_name */
	.ascii	"android/widget/Filterable"
	.zero	41
	.zero	2

	/* #90 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554571
	/* java_name */
	.ascii	"android/widget/LinearLayout"
	.zero	39
	.zero	2

	/* #91 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554566
	/* java_name */
	.ascii	"android/widget/ListAdapter"
	.zero	40
	.zero	2

	/* #92 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554572
	/* java_name */
	.ascii	"android/widget/ListView"
	.zero	43
	.zero	2

	/* #93 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554573
	/* java_name */
	.ascii	"android/widget/RadioButton"
	.zero	40
	.zero	2

	/* #94 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554574
	/* java_name */
	.ascii	"android/widget/Spinner"
	.zero	44
	.zero	2

	/* #95 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554568
	/* java_name */
	.ascii	"android/widget/SpinnerAdapter"
	.zero	37
	.zero	2

	/* #96 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554545
	/* java_name */
	.ascii	"android/widget/TextView"
	.zero	43
	.zero	2

	/* #97 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554570
	/* java_name */
	.ascii	"android/widget/ThemedSpinnerAdapter"
	.zero	31
	.zero	2

	/* #98 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554575
	/* java_name */
	.ascii	"android/widget/Toast"
	.zero	46
	.zero	2

	/* #99 */
	/* module_index */
	.long	0
	/* type_token_id */
	.long	33554434
	/* java_name */
	.ascii	"crc64835b3f8ef0122ba1/ActivityPlanoOC"
	.zero	29
	.zero	2

	/* #100 */
	/* module_index */
	.long	0
	/* type_token_id */
	.long	33554435
	/* java_name */
	.ascii	"crc64835b3f8ef0122ba1/Apresenta"
	.zero	35
	.zero	2

	/* #101 */
	/* module_index */
	.long	0
	/* type_token_id */
	.long	33554436
	/* java_name */
	.ascii	"crc64835b3f8ef0122ba1/Configurar"
	.zero	34
	.zero	2

	/* #102 */
	/* module_index */
	.long	0
	/* type_token_id */
	.long	33554437
	/* java_name */
	.ascii	"crc64835b3f8ef0122ba1/LeituraAT"
	.zero	35
	.zero	2

	/* #103 */
	/* module_index */
	.long	0
	/* type_token_id */
	.long	33554438
	/* java_name */
	.ascii	"crc64835b3f8ef0122ba1/MainActivity"
	.zero	32
	.zero	2

	/* #104 */
	/* module_index */
	.long	0
	/* type_token_id */
	.long	33554440
	/* java_name */
	.ascii	"crc64835b3f8ef0122ba1/Sobre"
	.zero	39
	.zero	2

	/* #105 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554907
	/* java_name */
	.ascii	"java/io/Closeable"
	.zero	49
	.zero	2

	/* #106 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554905
	/* java_name */
	.ascii	"java/io/FileInputStream"
	.zero	43
	.zero	2

	/* #107 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554909
	/* java_name */
	.ascii	"java/io/Flushable"
	.zero	49
	.zero	2

	/* #108 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554912
	/* java_name */
	.ascii	"java/io/IOException"
	.zero	47
	.zero	2

	/* #109 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554910
	/* java_name */
	.ascii	"java/io/InputStream"
	.zero	47
	.zero	2

	/* #110 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554915
	/* java_name */
	.ascii	"java/io/OutputStream"
	.zero	46
	.zero	2

	/* #111 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554917
	/* java_name */
	.ascii	"java/io/PrintWriter"
	.zero	47
	.zero	2

	/* #112 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554918
	/* java_name */
	.ascii	"java/io/Reader"
	.zero	52
	.zero	2

	/* #113 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554914
	/* java_name */
	.ascii	"java/io/Serializable"
	.zero	46
	.zero	2

	/* #114 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554920
	/* java_name */
	.ascii	"java/io/StringWriter"
	.zero	46
	.zero	2

	/* #115 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554921
	/* java_name */
	.ascii	"java/io/Writer"
	.zero	52
	.zero	2

	/* #116 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554872
	/* java_name */
	.ascii	"java/lang/Appendable"
	.zero	46
	.zero	2

	/* #117 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554874
	/* java_name */
	.ascii	"java/lang/AutoCloseable"
	.zero	43
	.zero	2

	/* #118 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554850
	/* java_name */
	.ascii	"java/lang/Boolean"
	.zero	49
	.zero	2

	/* #119 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554851
	/* java_name */
	.ascii	"java/lang/Byte"
	.zero	52
	.zero	2

	/* #120 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554875
	/* java_name */
	.ascii	"java/lang/CharSequence"
	.zero	44
	.zero	2

	/* #121 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554852
	/* java_name */
	.ascii	"java/lang/Character"
	.zero	47
	.zero	2

	/* #122 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554853
	/* java_name */
	.ascii	"java/lang/Class"
	.zero	51
	.zero	2

	/* #123 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554869
	/* java_name */
	.ascii	"java/lang/ClassCastException"
	.zero	38
	.zero	2

	/* #124 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554854
	/* java_name */
	.ascii	"java/lang/ClassNotFoundException"
	.zero	34
	.zero	2

	/* #125 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554878
	/* java_name */
	.ascii	"java/lang/Cloneable"
	.zero	47
	.zero	2

	/* #126 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554880
	/* java_name */
	.ascii	"java/lang/Comparable"
	.zero	46
	.zero	2

	/* #127 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554855
	/* java_name */
	.ascii	"java/lang/Double"
	.zero	50
	.zero	2

	/* #128 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554870
	/* java_name */
	.ascii	"java/lang/Error"
	.zero	51
	.zero	2

	/* #129 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554856
	/* java_name */
	.ascii	"java/lang/Exception"
	.zero	47
	.zero	2

	/* #130 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554857
	/* java_name */
	.ascii	"java/lang/Float"
	.zero	51
	.zero	2

	/* #131 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554881
	/* java_name */
	.ascii	"java/lang/IllegalArgumentException"
	.zero	32
	.zero	2

	/* #132 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554882
	/* java_name */
	.ascii	"java/lang/IllegalStateException"
	.zero	35
	.zero	2

	/* #133 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554883
	/* java_name */
	.ascii	"java/lang/IndexOutOfBoundsException"
	.zero	31
	.zero	2

	/* #134 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554859
	/* java_name */
	.ascii	"java/lang/Integer"
	.zero	49
	.zero	2

	/* #135 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554888
	/* java_name */
	.ascii	"java/lang/LinkageError"
	.zero	44
	.zero	2

	/* #136 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554860
	/* java_name */
	.ascii	"java/lang/Long"
	.zero	52
	.zero	2

	/* #137 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554889
	/* java_name */
	.ascii	"java/lang/NoClassDefFoundError"
	.zero	36
	.zero	2

	/* #138 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554890
	/* java_name */
	.ascii	"java/lang/NullPointerException"
	.zero	36
	.zero	2

	/* #139 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554891
	/* java_name */
	.ascii	"java/lang/Number"
	.zero	50
	.zero	2

	/* #140 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554861
	/* java_name */
	.ascii	"java/lang/Object"
	.zero	50
	.zero	2

	/* #141 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554885
	/* java_name */
	.ascii	"java/lang/Readable"
	.zero	48
	.zero	2

	/* #142 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554893
	/* java_name */
	.ascii	"java/lang/ReflectiveOperationException"
	.zero	28
	.zero	2

	/* #143 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554887
	/* java_name */
	.ascii	"java/lang/Runnable"
	.zero	48
	.zero	2

	/* #144 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554862
	/* java_name */
	.ascii	"java/lang/RuntimeException"
	.zero	40
	.zero	2

	/* #145 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554863
	/* java_name */
	.ascii	"java/lang/Short"
	.zero	51
	.zero	2

	/* #146 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554864
	/* java_name */
	.ascii	"java/lang/String"
	.zero	50
	.zero	2

	/* #147 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554866
	/* java_name */
	.ascii	"java/lang/Thread"
	.zero	50
	.zero	2

	/* #148 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554868
	/* java_name */
	.ascii	"java/lang/Throwable"
	.zero	47
	.zero	2

	/* #149 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554894
	/* java_name */
	.ascii	"java/lang/UnsupportedOperationException"
	.zero	27
	.zero	2

	/* #150 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554896
	/* java_name */
	.ascii	"java/lang/annotation/Annotation"
	.zero	35
	.zero	2

	/* #151 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554898
	/* java_name */
	.ascii	"java/lang/reflect/AnnotatedElement"
	.zero	32
	.zero	2

	/* #152 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554900
	/* java_name */
	.ascii	"java/lang/reflect/GenericDeclaration"
	.zero	30
	.zero	2

	/* #153 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554902
	/* java_name */
	.ascii	"java/lang/reflect/Type"
	.zero	44
	.zero	2

	/* #154 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554904
	/* java_name */
	.ascii	"java/lang/reflect/TypeVariable"
	.zero	36
	.zero	2

	/* #155 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554802
	/* java_name */
	.ascii	"java/net/InetSocketAddress"
	.zero	40
	.zero	2

	/* #156 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554803
	/* java_name */
	.ascii	"java/net/Proxy"
	.zero	52
	.zero	2

	/* #157 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554804
	/* java_name */
	.ascii	"java/net/ProxySelector"
	.zero	44
	.zero	2

	/* #158 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554806
	/* java_name */
	.ascii	"java/net/SocketAddress"
	.zero	44
	.zero	2

	/* #159 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554808
	/* java_name */
	.ascii	"java/net/URI"
	.zero	54
	.zero	2

	/* #160 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554823
	/* java_name */
	.ascii	"java/nio/Buffer"
	.zero	51
	.zero	2

	/* #161 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554827
	/* java_name */
	.ascii	"java/nio/ByteBuffer"
	.zero	47
	.zero	2

	/* #162 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554824
	/* java_name */
	.ascii	"java/nio/CharBuffer"
	.zero	47
	.zero	2

	/* #163 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554833
	/* java_name */
	.ascii	"java/nio/channels/ByteChannel"
	.zero	37
	.zero	2

	/* #164 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554835
	/* java_name */
	.ascii	"java/nio/channels/Channel"
	.zero	41
	.zero	2

	/* #165 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554830
	/* java_name */
	.ascii	"java/nio/channels/FileChannel"
	.zero	37
	.zero	2

	/* #166 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554837
	/* java_name */
	.ascii	"java/nio/channels/GatheringByteChannel"
	.zero	28
	.zero	2

	/* #167 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554839
	/* java_name */
	.ascii	"java/nio/channels/InterruptibleChannel"
	.zero	28
	.zero	2

	/* #168 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554841
	/* java_name */
	.ascii	"java/nio/channels/ReadableByteChannel"
	.zero	29
	.zero	2

	/* #169 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554843
	/* java_name */
	.ascii	"java/nio/channels/ScatteringByteChannel"
	.zero	27
	.zero	2

	/* #170 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554845
	/* java_name */
	.ascii	"java/nio/channels/SeekableByteChannel"
	.zero	29
	.zero	2

	/* #171 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554847
	/* java_name */
	.ascii	"java/nio/channels/WritableByteChannel"
	.zero	29
	.zero	2

	/* #172 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554848
	/* java_name */
	.ascii	"java/nio/channels/spi/AbstractInterruptibleChannel"
	.zero	16
	.zero	2

	/* #173 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554811
	/* java_name */
	.ascii	"java/security/KeyStore"
	.zero	44
	.zero	2

	/* #174 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554813
	/* java_name */
	.ascii	"java/security/KeyStore$LoadStoreParameter"
	.zero	25
	.zero	2

	/* #175 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554815
	/* java_name */
	.ascii	"java/security/KeyStore$ProtectionParameter"
	.zero	24
	.zero	2

	/* #176 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554816
	/* java_name */
	.ascii	"java/security/cert/Certificate"
	.zero	36
	.zero	2

	/* #177 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554818
	/* java_name */
	.ascii	"java/security/cert/CertificateFactory"
	.zero	29
	.zero	2

	/* #178 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554821
	/* java_name */
	.ascii	"java/security/cert/X509Certificate"
	.zero	32
	.zero	2

	/* #179 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554820
	/* java_name */
	.ascii	"java/security/cert/X509Extension"
	.zero	34
	.zero	2

	/* #180 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554765
	/* java_name */
	.ascii	"java/util/ArrayList"
	.zero	47
	.zero	2

	/* #181 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554754
	/* java_name */
	.ascii	"java/util/Collection"
	.zero	46
	.zero	2

	/* #182 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554756
	/* java_name */
	.ascii	"java/util/HashMap"
	.zero	49
	.zero	2

	/* #183 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554774
	/* java_name */
	.ascii	"java/util/HashSet"
	.zero	49
	.zero	2

	/* #184 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554810
	/* java_name */
	.ascii	"java/util/Iterator"
	.zero	48
	.zero	2

	/* #185 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554522
	/* java_name */
	.ascii	"javax/net/ssl/TrustManager"
	.zero	40
	.zero	2

	/* #186 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554525
	/* java_name */
	.ascii	"javax/net/ssl/TrustManagerFactory"
	.zero	33
	.zero	2

	/* #187 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554524
	/* java_name */
	.ascii	"javax/net/ssl/X509TrustManager"
	.zero	36
	.zero	2

	/* #188 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554944
	/* java_name */
	.ascii	"mono/android/TypeManager"
	.zero	42
	.zero	2

	/* #189 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554750
	/* java_name */
	.ascii	"mono/android/runtime/InputStreamAdapter"
	.zero	27
	.zero	2

	/* #190 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	0
	/* java_name */
	.ascii	"mono/android/runtime/JavaArray"
	.zero	36
	.zero	2

	/* #191 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554771
	/* java_name */
	.ascii	"mono/android/runtime/JavaObject"
	.zero	35
	.zero	2

	/* #192 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554789
	/* java_name */
	.ascii	"mono/android/runtime/OutputStreamAdapter"
	.zero	26
	.zero	2

	/* #193 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554580
	/* java_name */
	.ascii	"mono/android/view/View_OnClickListenerImplementor"
	.zero	17
	.zero	2

	/* #194 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554541
	/* java_name */
	.ascii	"mono/android/widget/AdapterView_OnItemSelectedListenerImplementor"
	.zero	1
	.zero	2

	/* #195 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554867
	/* java_name */
	.ascii	"mono/java/lang/RunnableImplementor"
	.zero	32
	.zero	2

	/* #196 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554518
	/* java_name */
	.ascii	"org/xmlpull/v1/XmlPullParser"
	.zero	38
	.zero	2

	/* #197 */
	/* module_index */
	.long	1
	/* type_token_id */
	.long	33554519
	/* java_name */
	.ascii	"org/xmlpull/v1/XmlPullParserException"
	.zero	29
	.zero	2

	.size	map_java, 15048
/* Java to managed map: END */


/* java_name_width: START */
	.section	.rodata.java_name_width,"a",%progbits
	.type	java_name_width, %object
	.p2align	2
	.global	java_name_width
java_name_width:
	.size	java_name_width, 4
	.long	68
/* java_name_width: END */
