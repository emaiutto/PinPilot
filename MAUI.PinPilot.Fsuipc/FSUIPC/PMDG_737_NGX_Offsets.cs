namespace FSUIPC;

public class PMDG_737_NGX_Offsets
{
	private static int ID;

	private object lockObject = new object();

	private string groupName;

	private byte classInstance;

	public Offset<byte> IRS_DisplaySelector { get; private set; }

	public Offset<byte> IRS_SysDisplay_R { get; private set; }

	public Offset<byte> IRS_annunGPS { get; private set; }

	public Offset<byte>[] IRS_annunALIGN { get; private set; }

	public Offset<byte>[] IRS_annunON_DC { get; private set; }

	public Offset<byte>[] IRS_annunFAULT { get; private set; }

	public Offset<byte>[] IRS_annunDC_FAIL { get; private set; }

	public Offset<byte>[] IRS_ModeSelector { get; private set; }

	public Offset<byte> IRS_aligned { get; private set; }

	public Offset<string> IRS_DisplayLeft { get; private set; }

	public Offset<string> IRS_DisplayRight { get; private set; }

	public Offset<byte> IRS_DisplayShowsDots { get; private set; }

	public Offset<byte> AFS_AutothrottleServosConnected { get; private set; }

	public Offset<byte> AFS_ControlsPitch { get; private set; }

	public Offset<byte> AFS_ControlsRoll { get; private set; }

	public Offset<byte> WARN_annunPSEU { get; private set; }

	public Offset<byte> COMM_ServiceInterphoneSw { get; private set; }

	public Offset<byte> LTS_DomeWhiteSw { get; private set; }

	public Offset<byte>[] ENG_EECSwitch { get; private set; }

	public Offset<byte>[] ENG_annunREVERSER { get; private set; }

	public Offset<byte>[] ENG_annunENGINE_CONTROL { get; private set; }

	public Offset<byte>[] ENG_annunALTN { get; private set; }

	public Offset<byte>[] ENG_StartValve { get; private set; }

	public Offset<byte> OXY_Needle { get; private set; }

	public Offset<byte> OXY_SwNormal { get; private set; }

	public Offset<byte> OXY_annunPASS_OXY_ON { get; private set; }

	public Offset<byte> GEAR_annunOvhdLEFT { get; private set; }

	public Offset<byte> GEAR_annunOvhdNOSE { get; private set; }

	public Offset<byte> GEAR_annunOvhdRIGHT { get; private set; }

	public Offset<byte> FLTREC_SwNormal { get; private set; }

	public Offset<byte> FLTREC_annunOFF { get; private set; }

	public Offset<byte> CVR_annunTEST { get; private set; }

	public Offset<byte>[] FCTL_FltControl_Sw { get; private set; }

	public Offset<byte>[] FCTL_Spoiler_Sw { get; private set; }

	public Offset<byte> FCTL_YawDamper_Sw { get; private set; }

	public Offset<byte> FCTL_AltnFlaps_Sw_ARM { get; private set; }

	public Offset<byte> FCTL_AltnFlaps_Control_Sw { get; private set; }

	public Offset<byte>[] FCTL_annunFC_LOW_PRESSURE { get; private set; }

	public Offset<byte> FCTL_annunYAW_DAMPER { get; private set; }

	public Offset<byte> FCTL_annunLOW_QUANTITY { get; private set; }

	public Offset<byte> FCTL_annunLOW_PRESSURE { get; private set; }

	public Offset<byte> FCTL_annunLOW_STBY_RUD_ON { get; private set; }

	public Offset<byte> FCTL_annunFEEL_DIFF_PRESS { get; private set; }

	public Offset<byte> FCTL_annunSPEED_TRIM_FAIL { get; private set; }

	public Offset<byte> FCTL_annunMACH_TRIM_FAIL { get; private set; }

	public Offset<byte> FCTL_annunAUTO_SLAT_FAIL { get; private set; }

	public Offset<byte> NAVDIS_VHFNavSelector { get; private set; }

	public Offset<byte> NAVDIS_IRSSelector { get; private set; }

	public Offset<byte> NAVDIS_FMCSelector { get; private set; }

	public Offset<byte> NAVDIS_SourceSelector { get; private set; }

	public Offset<byte> NAVDIS_ControlPaneSelector { get; private set; }

	public Offset<uint> ADF_StandbyFrequency { get; private set; }

	public Offset<float> FUEL_FuelTempNeedle { get; private set; }

	public Offset<byte> FUEL_CrossFeedSw { get; private set; }

	public Offset<byte>[] FUEL_PumpFwdSw { get; private set; }

	public Offset<byte>[] FUEL_PumpAftSw { get; private set; }

	public Offset<byte>[] FUEL_PumpCtrSw { get; private set; }

	public Offset<byte>[] FUEL_AuxFwd { get; private set; }

	public Offset<byte>[] FUEL_AuxAft { get; private set; }

	public Offset<byte> FUEL_FWDBleed { get; private set; }

	public Offset<byte> FUEL_AFTBleed { get; private set; }

	public Offset<byte> FUEL_GNDXfr { get; private set; }

	public Offset<byte>[] FUEL_annunENG_VALVE_CLOSED { get; private set; }

	public Offset<byte>[] FUEL_annunSPAR_VALVE_CLOSED { get; private set; }

	public Offset<byte>[] FUEL_annunFILTER_BYPASS { get; private set; }

	public Offset<byte> FUEL_annunXFEED_VALVE_OPEN { get; private set; }

	public Offset<byte>[] FUEL_annunLOWPRESS_Fwd { get; private set; }

	public Offset<byte>[] FUEL_annunLOWPRESS_Aft { get; private set; }

	public Offset<byte>[] FUEL_annunLOWPRESS_Ctr { get; private set; }

	public Offset<float> FUEL_QtyCenter { get; private set; }

	public Offset<float> FUEL_QtyLeft { get; private set; }

	public Offset<float> FUEL_QtyRight { get; private set; }

	public Offset<byte> ELEC_annunBAT_DISCHARGE { get; private set; }

	public Offset<byte> ELEC_annunTR_UNIT { get; private set; }

	public Offset<byte> ELEC_annunELEC { get; private set; }

	public Offset<byte> ELEC_DCMeterSelector { get; private set; }

	public Offset<byte> ELEC_ACMeterSelector { get; private set; }

	public Offset<byte> ELEC_BatSelector { get; private set; }

	public Offset<byte> ELEC_CabUtilSw { get; private set; }

	public Offset<byte> ELEC_IFEPassSeatSw { get; private set; }

	public Offset<byte>[] ELEC_annunDRIVE { get; private set; }

	public Offset<byte> ELEC_annunSTANDBY_POWER_OFF { get; private set; }

	public Offset<byte>[] ELEC_IDGDisconnectSw { get; private set; }

	public Offset<byte> ELEC_StandbyPowerSelector { get; private set; }

	public Offset<byte> ELEC_annunGRD_POWER_AVAILABLE { get; private set; }

	public Offset<byte> ELEC_GrdPwrSw { get; private set; }

	public Offset<byte> ELEC_BusTransSw_AUTO { get; private set; }

	public Offset<byte>[] ELEC_GenSw { get; private set; }

	public Offset<byte>[] ELEC_APUGenSw { get; private set; }

	public Offset<byte>[] ELEC_annunTRANSFER_BUS_OFF { get; private set; }

	public Offset<byte>[] ELEC_annunSOURCE_OFF { get; private set; }

	public Offset<byte>[] ELEC_annunGEN_BUS_OFF { get; private set; }

	public Offset<byte> ELEC_annunAPU_GEN_OFF_BUS { get; private set; }

	public Offset<string> ELEC_MeterDisplayTop { get; private set; }

	public Offset<string> ELEC_MeterDisplayBottom { get; private set; }

	public Offset<byte>[] ELEC_BusPowered { get; private set; }

	public Offset<float> APU_EGTNeedle { get; private set; }

	public Offset<byte> APU_annunMAINT { get; private set; }

	public Offset<byte> APU_annunLOW_OIL_PRESSURE { get; private set; }

	public Offset<byte> APU_annunFAULT { get; private set; }

	public Offset<byte> APU_annunOVERSPEED { get; private set; }

	public Offset<byte> OH_WiperLSelector { get; private set; }

	public Offset<byte> OH_WiperRSelector { get; private set; }

	public Offset<byte> LTS_CircuitBreakerKnob { get; private set; }

	public Offset<byte> LTS_OvereadPanelKnob { get; private set; }

	public Offset<byte> AIR_EquipCoolingSupplyNORM { get; private set; }

	public Offset<byte> AIR_EquipCoolingExhaustNORM { get; private set; }

	public Offset<byte> AIR_annunEquipCoolingSupplyOFF { get; private set; }

	public Offset<byte> AIR_annunEquipCoolingExhaustOFF { get; private set; }

	public Offset<byte> LTS_annunEmerNOT_ARMED { get; private set; }

	public Offset<byte> LTS_EmerExitSelector { get; private set; }

	public Offset<byte> COMM_NoSmokingSelector { get; private set; }

	public Offset<byte> COMM_FastenBeltsSelector { get; private set; }

	public Offset<byte> COMM_annunCALL { get; private set; }

	public Offset<byte> COMM_annunPA_IN_USE { get; private set; }

	public Offset<byte>[] ICE_annunOVERHEAT { get; private set; }

	public Offset<byte>[] ICE_annunON { get; private set; }

	public Offset<byte>[] ICE_WindowHeatSw { get; private set; }

	public Offset<byte> ICE_annunCAPT_PITOT { get; private set; }

	public Offset<byte> ICE_annunL_ELEV_PITOT { get; private set; }

	public Offset<byte> ICE_annunL_ALPHA_VANE { get; private set; }

	public Offset<byte> ICE_annunL_TEMP_PROBE { get; private set; }

	public Offset<byte> ICE_annunFO_PITOT { get; private set; }

	public Offset<byte> ICE_annunR_ELEV_PITOT { get; private set; }

	public Offset<byte> ICE_annunR_ALPHA_VANE { get; private set; }

	public Offset<byte> ICE_annunAUX_PITOT { get; private set; }

	public Offset<byte>[] ICE_TestProbeHeatSw { get; private set; }

	public Offset<byte>[] ICE_annunVALVE_OPEN { get; private set; }

	public Offset<byte>[] ICE_annunCOWL_ANTI_ICE { get; private set; }

	public Offset<byte>[] ICE_annunCOWL_VALVE_OPEN { get; private set; }

	public Offset<byte> ICE_WingAntiIceSw { get; private set; }

	public Offset<byte>[] ICE_EngAntiIceSw { get; private set; }

	public Offset<int> ICE_WindowHeatTestSw { get; private set; }

	public Offset<byte>[] HYD_annunLOW_PRESS_eng { get; private set; }

	public Offset<byte>[] HYD_annunLOW_PRESS_elec { get; private set; }

	public Offset<byte>[] HYD_annunOVERHEAT_elec { get; private set; }

	public Offset<byte>[] HYD_PumpSw_eng { get; private set; }

	public Offset<byte>[] HYD_PumpSw_elec { get; private set; }

	public Offset<byte> AIR_TempSourceSelector { get; private set; }

	public Offset<byte> AIR_TrimAirSwitch { get; private set; }

	public Offset<byte>[] AIR_annunZoneTemp { get; private set; }

	public Offset<byte> AIR_annunDualBleed { get; private set; }

	public Offset<byte> AIR_annunRamDoorL { get; private set; }

	public Offset<byte> AIR_annunRamDoorR { get; private set; }

	public Offset<byte>[] AIR_RecircFanSwitch { get; private set; }

	public Offset<byte>[] AIR_PackSwitch { get; private set; }

	public Offset<byte>[] AIR_BleedAirSwitch { get; private set; }

	public Offset<byte> AIR_APUBleedAirSwitch { get; private set; }

	public Offset<byte> AIR_IsolationValveSwitch { get; private set; }

	public Offset<byte>[] AIR_annunPackTripOff { get; private set; }

	public Offset<byte>[] AIR_annunWingBodyOverheat { get; private set; }

	public Offset<byte>[] AIR_annunBleedTripOff { get; private set; }

	public Offset<byte> AIR_annunAUTO_FAIL { get; private set; }

	public Offset<byte> AIR_annunOFFSCHED_DESCENT { get; private set; }

	public Offset<byte> AIR_annunALTN { get; private set; }

	public Offset<byte> AIR_annunMANUAL { get; private set; }

	public Offset<float>[] AIR_DuctPress { get; private set; }

	public Offset<float>[] AIR_DuctPressNeedle { get; private set; }

	public Offset<float> AIR_CabinAltNeedle { get; private set; }

	public Offset<float> AIR_CabinDPNeedle { get; private set; }

	public Offset<float> AIR_CabinVSNeedle { get; private set; }

	public Offset<float> AIR_CabinValveNeedle { get; private set; }

	public Offset<float> AIR_TemperatureNeedle { get; private set; }

	public Offset<string> AIR_DisplayFltAlt { get; private set; }

	public Offset<string> AIR_DisplayLandAlt { get; private set; }

	public Offset<byte> DOOR_annunFWD_ENTRY { get; private set; }

	public Offset<byte> DOOR_annunFWD_SERVICE { get; private set; }

	public Offset<byte> DOOR_annunAIRSTAIR { get; private set; }

	public Offset<byte> DOOR_annunLEFT_FWD_OVERWING { get; private set; }

	public Offset<byte> DOOR_annunRIGHT_FWD_OVERWING { get; private set; }

	public Offset<byte> DOOR_annunFWD_CARGO { get; private set; }

	public Offset<byte> DOOR_annunEQUIP { get; private set; }

	public Offset<byte> DOOR_annunLEFT_AFT_OVERWING { get; private set; }

	public Offset<byte> DOOR_annunRIGHT_AFT_OVERWING { get; private set; }

	public Offset<byte> DOOR_annunAFT_CARGO { get; private set; }

	public Offset<byte> DOOR_annunAFT_ENTRY { get; private set; }

	public Offset<byte> DOOR_annunAFT_SERVICE { get; private set; }

	public Offset<uint> AIR_FltAltWindow { get; private set; }

	public Offset<uint> AIR_LandAltWindow { get; private set; }

	public Offset<uint> AIR_OutflowValveSwitch { get; private set; }

	public Offset<uint> AIR_PressurizationModeSelector { get; private set; }

	public Offset<byte>[] LTS_LandingLtRetractableSw { get; private set; }

	public Offset<byte>[] LTS_LandingLtFixedSw { get; private set; }

	public Offset<byte>[] LTS_RunwayTurnoffSw { get; private set; }

	public Offset<byte> LTS_TaxiSw { get; private set; }

	public Offset<byte> APU_Selector { get; private set; }

	public Offset<byte>[] ENG_StartSelector { get; private set; }

	public Offset<byte> ENG_IgnitionSelector { get; private set; }

	public Offset<byte> LTS_LogoSw { get; private set; }

	public Offset<byte> LTS_PositionSw { get; private set; }

	public Offset<byte> LTS_AntiCollisionSw { get; private set; }

	public Offset<byte> LTS_WingSw { get; private set; }

	public Offset<byte> LTS_WheelWellSw { get; private set; }

	public Offset<byte>[] WARN_annunFIRE_WARN { get; private set; }

	public Offset<byte>[] WARN_annunMASTER_CAUTION { get; private set; }

	public Offset<byte> WARN_annunFLT_CONT { get; private set; }

	public Offset<byte> WARN_annunIRS { get; private set; }

	public Offset<byte> WARN_annunFUEL { get; private set; }

	public Offset<byte> WARN_annunELEC { get; private set; }

	public Offset<byte> WARN_annunAPU { get; private set; }

	public Offset<byte> WARN_annunOVHT_DET { get; private set; }

	public Offset<byte> WARN_annunANTI_ICE { get; private set; }

	public Offset<byte> WARN_annunHYD { get; private set; }

	public Offset<byte> WARN_annunDOORS { get; private set; }

	public Offset<byte> WARN_annunENG { get; private set; }

	public Offset<byte> WARN_annunOVERHEAD { get; private set; }

	public Offset<byte> WARN_annunAIR_COND { get; private set; }

	public Offset<byte>[] EFIS_MinsSelBARO { get; private set; }

	public Offset<byte>[] EFIS_BaroSelHPA { get; private set; }

	public Offset<byte>[] EFIS_VORADFSel1 { get; private set; }

	public Offset<byte>[] EFIS_VORADFSel2 { get; private set; }

	public Offset<byte>[] EFIS_ModeSel { get; private set; }

	public Offset<byte>[] EFIS_RangeSel { get; private set; }

	public Offset<ushort>[] MCP_Course { get; private set; }

	public Offset<float> MCP_IASMach { get; private set; }

	public Offset<byte> MCP_IASBlank { get; private set; }

	public Offset<byte> MCP_IASOverspeedFlash { get; private set; }

	public Offset<byte> MCP_IASUnderspeedFlash { get; private set; }

	public Offset<ushort> MCP_Heading { get; private set; }

	public Offset<ushort> MCP_Altitude { get; private set; }

	public Offset<ushort> MCP_VertSpeed { get; private set; }

	public Offset<byte> MCP_VertSpeedBlank { get; private set; }

	public Offset<byte>[] MCP_FDSw { get; private set; }

	public Offset<byte> MCP_ATArmSw { get; private set; }

	public Offset<byte> MCP_BankLimitSel { get; private set; }

	public Offset<byte> MCP_DisengageBar { get; private set; }

	public Offset<byte>[] MCP_annunFD { get; private set; }

	public Offset<byte> MCP_annunATArm { get; private set; }

	public Offset<byte> MCP_annunN1 { get; private set; }

	public Offset<byte> MCP_annunSPEED { get; private set; }

	public Offset<byte> MCP_annunVNAV { get; private set; }

	public Offset<byte> MCP_annunLVL_CHG { get; private set; }

	public Offset<byte> MCP_annunHDG_SEL { get; private set; }

	public Offset<byte> MCP_annunLNAV { get; private set; }

	public Offset<byte> MCP_annunVOR_LOC { get; private set; }

	public Offset<byte> MCP_annunAPP { get; private set; }

	public Offset<byte> MCP_annunALT_HOLD { get; private set; }

	public Offset<byte> MCP_annunVS { get; private set; }

	public Offset<byte> MCP_annunCMD_A { get; private set; }

	public Offset<byte> MCP_annunCWS_A { get; private set; }

	public Offset<byte> MCP_annunCMD_B { get; private set; }

	public Offset<byte> MCP_annunCWS_B { get; private set; }

	public Offset<byte> MCP_indication_powered { get; private set; }

	public Offset<byte> MAIN_NoseWheelSteeringSwNORM { get; private set; }

	public Offset<byte>[] MAIN_annunBELOW_GS { get; private set; }

	public Offset<byte>[] MAIN_MainPanelDUSel { get; private set; }

	public Offset<byte>[] MAIN_LowerDUSel { get; private set; }

	public Offset<byte>[] MAIN_annunAP { get; private set; }

	public Offset<byte>[] MAIN_annunAP_Amber { get; private set; }

	public Offset<byte>[] MAIN_annunAT { get; private set; }

	public Offset<byte>[] MAIN_annunAT_Amber { get; private set; }

	public Offset<byte>[] MAIN_annunFMC { get; private set; }

	public Offset<byte>[] MAIN_DisengageTestSelector { get; private set; }

	public Offset<byte> MAIN_annunSPEEDBRAKE_ARMED { get; private set; }

	public Offset<byte> MAIN_annunSPEEDBRAKE_DO_NOT_ARM { get; private set; }

	public Offset<byte> MAIN_annunSPEEDBRAKE_EXTENDED { get; private set; }

	public Offset<byte> MAIN_annunSTAB_OUT_OF_TRIM { get; private set; }

	public Offset<byte> MAIN_LightsSelector { get; private set; }

	public Offset<byte> MAIN_RMISelector1_VOR { get; private set; }

	public Offset<byte> MAIN_RMISelector2_VOR { get; private set; }

	public Offset<byte> MAIN_N1SetSelector { get; private set; }

	public Offset<byte> MAIN_SpdRefSelector { get; private set; }

	public Offset<byte> MAIN_FuelFlowSelector { get; private set; }

	public Offset<byte> MAIN_AutobrakeSelector { get; private set; }

	public Offset<byte> MAIN_annunANTI_SKID_INOP { get; private set; }

	public Offset<byte> MAIN_annunAUTO_BRAKE_DISARM { get; private set; }

	public Offset<byte> MAIN_annunLE_FLAPS_TRANSIT { get; private set; }

	public Offset<byte> MAIN_annunLE_FLAPS_EXT { get; private set; }

	public Offset<float>[] MAIN_TEFlapsNeedle { get; private set; }

	public Offset<byte>[] MAIN_annunGEAR_transit { get; private set; }

	public Offset<byte>[] MAIN_annunGEAR_locked { get; private set; }

	public Offset<byte> MAIN_GearLever { get; private set; }

	public Offset<float> MAIN_BrakePressNeedle { get; private set; }

	public Offset<byte> MAIN_annunCABIN_ALTITUDE { get; private set; }

	public Offset<byte> MAIN_annunTAKEOFF_CONFIG { get; private set; }

	public Offset<byte> HGS_annun_AIII { get; private set; }

	public Offset<byte> HGS_annun_NO_AIII { get; private set; }

	public Offset<byte> HGS_annun_FLARE { get; private set; }

	public Offset<byte> HGS_annun_RO { get; private set; }

	public Offset<byte> HGS_annun_RO_CTN { get; private set; }

	public Offset<byte> HGS_annun_RO_ARM { get; private set; }

	public Offset<byte> HGS_annun_TO { get; private set; }

	public Offset<byte> HGS_annun_TO_CTN { get; private set; }

	public Offset<byte> HGS_annun_APCH { get; private set; }

	public Offset<byte> HGS_annun_TO_WARN { get; private set; }

	public Offset<byte> HGS_annun_Bar { get; private set; }

	public Offset<byte> HGS_annun_FAIL { get; private set; }

	public Offset<byte>[] LTS_MainPanelKnob { get; private set; }

	public Offset<byte> LTS_BackgroundKnob { get; private set; }

	public Offset<byte> LTS_AFDSFloodKnob { get; private set; }

	public Offset<byte>[] LTS_OutbdDUBrtKnob { get; private set; }

	public Offset<byte>[] LTS_InbdDUBrtKnob { get; private set; }

	public Offset<byte>[] LTS_InbdDUMapBrtKnob { get; private set; }

	public Offset<byte> LTS_UpperDUBrtKnob { get; private set; }

	public Offset<byte> LTS_LowerDUBrtKnob { get; private set; }

	public Offset<byte> LTS_LowerDUMapBrtKnob { get; private set; }

	public Offset<byte> GPWS_annunINOP { get; private set; }

	public Offset<byte> GPWS_FlapInhibitSw_NORM { get; private set; }

	public Offset<byte> GPWS_GearInhibitSw_NORM { get; private set; }

	public Offset<byte> GPWS_TerrInhibitSw_NORM { get; private set; }

	public Offset<byte>[] CDU_annunEXEC { get; private set; }

	public Offset<byte>[] CDU_annunCALL { get; private set; }

	public Offset<byte>[] CDU_annunFAIL { get; private set; }

	public Offset<byte>[] CDU_annunMSG { get; private set; }

	public Offset<byte>[] CDU_annunOFST { get; private set; }

	public Offset<byte>[] CDU_BrtKnob { get; private set; }

	public Offset<byte> COMM_Attend_PressCount { get; private set; }

	public Offset<byte> COMM_GrdCall_PressCount { get; private set; }

	public Offset<byte>[] COMM_SelectedMic { get; private set; }

	public Offset<uint>[] COMM_ReceiverSwitches { get; private set; }

	public Offset<byte> TRIM_StabTrimMainElecSw_NORMAL { get; private set; }

	public Offset<byte> TRIM_StabTrimAutoPilotSw_NORMAL { get; private set; }

	public Offset<byte> PED_annunParkingBrake { get; private set; }

	public Offset<byte>[] FIRE_OvhtDetSw { get; private set; }

	public Offset<byte>[] FIRE_annunENG_OVERHEAT { get; private set; }

	public Offset<byte> FIRE_DetTestSw { get; private set; }

	public Offset<byte>[] FIRE_HandlePos { get; private set; }

	public Offset<byte>[] FIRE_HandleIlluminated { get; private set; }

	public Offset<byte> FIRE_annunWHEEL_WELL { get; private set; }

	public Offset<byte> FIRE_annunFAULT { get; private set; }

	public Offset<byte> FIRE_annunAPU_DET_INOP { get; private set; }

	public Offset<byte> FIRE_annunAPU_BOTTLE_DISCHARGE { get; private set; }

	public Offset<byte>[] FIRE_annunBOTTLE_DISCHARGE { get; private set; }

	public Offset<byte> FIRE_ExtinguisherTestSw { get; private set; }

	public Offset<byte>[] FIRE_annunExtinguisherTest { get; private set; }

	public Offset<byte>[] CARGO_annunExtTest { get; private set; }

	public Offset<byte>[] CARGO_DetSelect { get; private set; }

	public Offset<byte>[] CARGO_ArmedSw { get; private set; }

	public Offset<byte> CARGO_annunFWD { get; private set; }

	public Offset<byte> CARGO_annunAFT { get; private set; }

	public Offset<byte> CARGO_annunDETECTOR_FAULT { get; private set; }

	public Offset<byte> CARGO_annunDISCH { get; private set; }

	public Offset<byte> HGS_annunRWY { get; private set; }

	public Offset<byte> HGS_annunGS { get; private set; }

	public Offset<byte> HGS_annunFAULT { get; private set; }

	public Offset<byte> HGS_annunCLR { get; private set; }

	public Offset<byte> XPDR_XpndrSelector_2 { get; private set; }

	public Offset<byte> XPDR_AltSourceSel_2 { get; private set; }

	public Offset<byte> XPDR_ModeSel { get; private set; }

	public Offset<byte> XPDR_annunFAIL { get; private set; }

	public Offset<byte> LTS_PedFloodKnob { get; private set; }

	public Offset<byte> LTS_PedPanelKnob { get; private set; }

	public Offset<byte> TRIM_StabTrimSw_NORMAL { get; private set; }

	public Offset<byte> PED_annunLOCK_FAIL { get; private set; }

	public Offset<byte> PED_annunAUTO_UNLK { get; private set; }

	public Offset<byte> PED_FltDkDoorSel { get; private set; }

	public Offset<byte> FMC_TakeoffFlaps { get; private set; }

	public Offset<byte> FMC_V1 { get; private set; }

	public Offset<byte> FMC_VR { get; private set; }

	public Offset<byte> FMC_V2 { get; private set; }

	public Offset<byte> FMC_LandingFlaps { get; private set; }

	public Offset<byte> FMC_LandingVREF { get; private set; }

	public Offset<ushort> FMC_CruiseAlt { get; private set; }

	public Offset<ushort> FMC_LandingAltitude { get; private set; }

	public Offset<ushort> FMC_TransitionAlt { get; private set; }

	public Offset<ushort> FMC_TransitionLevel { get; private set; }

	public Offset<byte> FMC_PerfInputComplete { get; private set; }

	public Offset<float> FMC_DistanceToTOD { get; private set; }

	public Offset<float> FMC_DistanceToDest { get; private set; }

	public Offset<string> FMC_flightNumber { get; private set; }

	public Offset<byte> AircraftModel { get; private set; }

	public Offset<byte> WeightInKg { get; private set; }

	public Offset<byte> GPWS_V1CallEnabled { get; private set; }

	public Offset<byte> GroundConnAvailable { get; private set; }

	public Offset<byte> AircraftMode { get; private set; }

	public PMDG_737_NGX_Offsets()
		: this(0)
	{
	}

	public PMDG_737_NGX_Offsets(int ClassInstance)
	{
		ID++;
		groupName = "~~~ PMDG 737 OFFSETS " + ID + " ~~~";
		if (FSUIPCConnection.IsConnectionOpenForClass(classInstance))
		{
			if (FSUIPCConnection.FlightSimVersionForClass(classInstance).Simulator == FlightSim.MSFS)
			{
				instatiate_MSFS_Offsets();
			}
			else
			{
				instatiate_FSX_P3D_Offsets();
			}
			return;
		}
		if (classInstance == 0)
		{
			throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "The connection to FSUIPC is not open.");
		}
		throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "The connection to class instance " + classInstance.ToString("D2") + " of WideClient.exe is not open.");
	}

	private void instatiate_MSFS_Offsets()
	{
		IRS_DisplaySelector = new Offset<byte>(groupName, 25632);
		IRS_SysDisplay_R = new Offset<byte>(groupName, 25633);
		IRS_annunGPS = new Offset<byte>(groupName, 25634);
		IRS_annunALIGN = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25635),
			new Offset<byte>(groupName, 25636)
		};
		IRS_annunON_DC = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25637),
			new Offset<byte>(groupName, 25638)
		};
		IRS_annunFAULT = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25639),
			new Offset<byte>(groupName, 25640)
		};
		IRS_annunDC_FAIL = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25641),
			new Offset<byte>(groupName, 25642)
		};
		IRS_ModeSelector = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25643),
			new Offset<byte>(groupName, 25644)
		};
		IRS_aligned = new Offset<byte>(groupName, 25645);
		IRS_DisplayLeft = new Offset<string>(groupName, 25646, 7);
		IRS_DisplayRight = new Offset<string>(groupName, 25653, 8);
		IRS_DisplayShowsDots = new Offset<byte>(groupName, 25661);
		AFS_AutothrottleServosConnected = new Offset<byte>(groupName, 25662);
		AFS_ControlsPitch = new Offset<byte>(groupName, 25663);
		AFS_ControlsRoll = new Offset<byte>(groupName, 25664);
		WARN_annunPSEU = new Offset<byte>(groupName, 25665);
		COMM_ServiceInterphoneSw = new Offset<byte>(groupName, 25666);
		LTS_DomeWhiteSw = new Offset<byte>(groupName, 25667);
		ENG_EECSwitch = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25668),
			new Offset<byte>(groupName, 25669)
		};
		ENG_annunREVERSER = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25670),
			new Offset<byte>(groupName, 25671)
		};
		ENG_annunENGINE_CONTROL = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25672),
			new Offset<byte>(groupName, 25673)
		};
		ENG_annunALTN = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25674),
			new Offset<byte>(groupName, 25675)
		};
		ENG_StartValve = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25676),
			new Offset<byte>(groupName, 25677)
		};
		OXY_Needle = new Offset<byte>(groupName, 25678);
		OXY_SwNormal = new Offset<byte>(groupName, 25679);
		OXY_annunPASS_OXY_ON = new Offset<byte>(groupName, 25680);
		GEAR_annunOvhdLEFT = new Offset<byte>(groupName, 25681);
		GEAR_annunOvhdNOSE = new Offset<byte>(groupName, 25682);
		GEAR_annunOvhdRIGHT = new Offset<byte>(groupName, 25683);
		FLTREC_SwNormal = new Offset<byte>(groupName, 25684);
		FLTREC_annunOFF = new Offset<byte>(groupName, 25685);
		CVR_annunTEST = new Offset<byte>(groupName, 25686);
		FCTL_FltControl_Sw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25687),
			new Offset<byte>(groupName, 25688)
		};
		FCTL_Spoiler_Sw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25689),
			new Offset<byte>(groupName, 25690)
		};
		FCTL_YawDamper_Sw = new Offset<byte>(groupName, 25691);
		FCTL_AltnFlaps_Sw_ARM = new Offset<byte>(groupName, 25692);
		FCTL_AltnFlaps_Control_Sw = new Offset<byte>(groupName, 25693);
		FCTL_annunFC_LOW_PRESSURE = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25694),
			new Offset<byte>(groupName, 25695)
		};
		FCTL_annunYAW_DAMPER = new Offset<byte>(groupName, 25696);
		FCTL_annunLOW_QUANTITY = new Offset<byte>(groupName, 25697);
		FCTL_annunLOW_PRESSURE = new Offset<byte>(groupName, 25698);
		FCTL_annunLOW_STBY_RUD_ON = new Offset<byte>(groupName, 25699);
		FCTL_annunFEEL_DIFF_PRESS = new Offset<byte>(groupName, 25700);
		FCTL_annunSPEED_TRIM_FAIL = new Offset<byte>(groupName, 25701);
		FCTL_annunMACH_TRIM_FAIL = new Offset<byte>(groupName, 25702);
		FCTL_annunAUTO_SLAT_FAIL = new Offset<byte>(groupName, 25703);
		NAVDIS_VHFNavSelector = new Offset<byte>(groupName, 25704);
		NAVDIS_IRSSelector = new Offset<byte>(groupName, 25705);
		NAVDIS_FMCSelector = new Offset<byte>(groupName, 25706);
		NAVDIS_SourceSelector = new Offset<byte>(groupName, 25707);
		NAVDIS_ControlPaneSelector = new Offset<byte>(groupName, 25708);
		ADF_StandbyFrequency = new Offset<uint>(groupName, 25712);
		FUEL_FuelTempNeedle = new Offset<float>(groupName, 25716);
		FUEL_CrossFeedSw = new Offset<byte>(groupName, 25720);
		FUEL_PumpFwdSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25721),
			new Offset<byte>(groupName, 25722)
		};
		FUEL_PumpAftSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25723),
			new Offset<byte>(groupName, 25724)
		};
		FUEL_PumpCtrSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25725),
			new Offset<byte>(groupName, 25726)
		};
		FUEL_AuxFwd = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25727),
			new Offset<byte>(groupName, 25728)
		};
		FUEL_AuxAft = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25729),
			new Offset<byte>(groupName, 25730)
		};
		FUEL_FWDBleed = new Offset<byte>(groupName, 25731);
		FUEL_AFTBleed = new Offset<byte>(groupName, 25732);
		FUEL_GNDXfr = new Offset<byte>(groupName, 25733);
		FUEL_annunENG_VALVE_CLOSED = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25734),
			new Offset<byte>(groupName, 25735)
		};
		FUEL_annunSPAR_VALVE_CLOSED = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25736),
			new Offset<byte>(groupName, 25737)
		};
		FUEL_annunFILTER_BYPASS = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25738),
			new Offset<byte>(groupName, 25739)
		};
		FUEL_annunXFEED_VALVE_OPEN = new Offset<byte>(groupName, 25740);
		FUEL_annunLOWPRESS_Fwd = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25741),
			new Offset<byte>(groupName, 25742)
		};
		FUEL_annunLOWPRESS_Aft = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25743),
			new Offset<byte>(groupName, 25744)
		};
		FUEL_annunLOWPRESS_Ctr = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25745),
			new Offset<byte>(groupName, 25746)
		};
		FUEL_QtyCenter = new Offset<float>(groupName, 25748);
		FUEL_QtyLeft = new Offset<float>(groupName, 25752);
		FUEL_QtyRight = new Offset<float>(groupName, 25756);
		ELEC_annunBAT_DISCHARGE = new Offset<byte>(groupName, 25760);
		ELEC_annunTR_UNIT = new Offset<byte>(groupName, 25761);
		ELEC_annunELEC = new Offset<byte>(groupName, 25762);
		ELEC_DCMeterSelector = new Offset<byte>(groupName, 25763);
		ELEC_ACMeterSelector = new Offset<byte>(groupName, 25764);
		ELEC_BatSelector = new Offset<byte>(groupName, 25765);
		ELEC_CabUtilSw = new Offset<byte>(groupName, 25766);
		ELEC_IFEPassSeatSw = new Offset<byte>(groupName, 25767);
		ELEC_annunDRIVE = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25768),
			new Offset<byte>(groupName, 25769)
		};
		ELEC_annunSTANDBY_POWER_OFF = new Offset<byte>(groupName, 25770);
		ELEC_IDGDisconnectSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25771),
			new Offset<byte>(groupName, 25772)
		};
		ELEC_StandbyPowerSelector = new Offset<byte>(groupName, 25773);
		ELEC_annunGRD_POWER_AVAILABLE = new Offset<byte>(groupName, 25774);
		ELEC_GrdPwrSw = new Offset<byte>(groupName, 25775);
		ELEC_BusTransSw_AUTO = new Offset<byte>(groupName, 25776);
		ELEC_GenSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25777),
			new Offset<byte>(groupName, 25778)
		};
		ELEC_APUGenSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25779),
			new Offset<byte>(groupName, 25780)
		};
		ELEC_annunTRANSFER_BUS_OFF = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25781),
			new Offset<byte>(groupName, 25782)
		};
		ELEC_annunSOURCE_OFF = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25783),
			new Offset<byte>(groupName, 25784)
		};
		ELEC_annunGEN_BUS_OFF = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25785),
			new Offset<byte>(groupName, 25786)
		};
		ELEC_annunAPU_GEN_OFF_BUS = new Offset<byte>(groupName, 25787);
		ELEC_MeterDisplayTop = new Offset<string>(groupName, 25788, 13);
		ELEC_MeterDisplayBottom = new Offset<string>(groupName, 25801, 13);
		ELEC_BusPowered = new Offset<byte>[16]
		{
			new Offset<byte>(groupName, 25814),
			new Offset<byte>(groupName, 25815),
			new Offset<byte>(groupName, 25816),
			new Offset<byte>(groupName, 25817),
			new Offset<byte>(groupName, 25818),
			new Offset<byte>(groupName, 25819),
			new Offset<byte>(groupName, 25820),
			new Offset<byte>(groupName, 25821),
			new Offset<byte>(groupName, 25822),
			new Offset<byte>(groupName, 25823),
			new Offset<byte>(groupName, 25824),
			new Offset<byte>(groupName, 25825),
			new Offset<byte>(groupName, 25826),
			new Offset<byte>(groupName, 25827),
			new Offset<byte>(groupName, 25828),
			new Offset<byte>(groupName, 25829)
		};
		APU_EGTNeedle = new Offset<float>(groupName, 25832);
		APU_annunMAINT = new Offset<byte>(groupName, 25836);
		APU_annunLOW_OIL_PRESSURE = new Offset<byte>(groupName, 25837);
		APU_annunFAULT = new Offset<byte>(groupName, 25838);
		APU_annunOVERSPEED = new Offset<byte>(groupName, 25839);
		OH_WiperLSelector = new Offset<byte>(groupName, 25840);
		OH_WiperRSelector = new Offset<byte>(groupName, 25841);
		LTS_CircuitBreakerKnob = new Offset<byte>(groupName, 25842);
		LTS_OvereadPanelKnob = new Offset<byte>(groupName, 25843);
		AIR_EquipCoolingSupplyNORM = new Offset<byte>(groupName, 25844);
		AIR_EquipCoolingExhaustNORM = new Offset<byte>(groupName, 25845);
		AIR_annunEquipCoolingSupplyOFF = new Offset<byte>(groupName, 25846);
		AIR_annunEquipCoolingExhaustOFF = new Offset<byte>(groupName, 25847);
		LTS_annunEmerNOT_ARMED = new Offset<byte>(groupName, 25848);
		LTS_EmerExitSelector = new Offset<byte>(groupName, 25849);
		COMM_NoSmokingSelector = new Offset<byte>(groupName, 25850);
		COMM_FastenBeltsSelector = new Offset<byte>(groupName, 25851);
		COMM_annunCALL = new Offset<byte>(groupName, 25852);
		COMM_annunPA_IN_USE = new Offset<byte>(groupName, 25853);
		ICE_annunOVERHEAT = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25854),
			new Offset<byte>(groupName, 25855),
			new Offset<byte>(groupName, 25856),
			new Offset<byte>(groupName, 25857)
		};
		ICE_annunON = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25858),
			new Offset<byte>(groupName, 25859),
			new Offset<byte>(groupName, 25860),
			new Offset<byte>(groupName, 25861)
		};
		ICE_WindowHeatSw = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25862),
			new Offset<byte>(groupName, 25863),
			new Offset<byte>(groupName, 25864),
			new Offset<byte>(groupName, 25865)
		};
		ICE_annunCAPT_PITOT = new Offset<byte>(groupName, 25866);
		ICE_annunL_ELEV_PITOT = new Offset<byte>(groupName, 25867);
		ICE_annunL_ALPHA_VANE = new Offset<byte>(groupName, 25868);
		ICE_annunL_TEMP_PROBE = new Offset<byte>(groupName, 25869);
		ICE_annunFO_PITOT = new Offset<byte>(groupName, 25870);
		ICE_annunR_ELEV_PITOT = new Offset<byte>(groupName, 25871);
		ICE_annunR_ALPHA_VANE = new Offset<byte>(groupName, 25872);
		ICE_annunAUX_PITOT = new Offset<byte>(groupName, 25873);
		ICE_TestProbeHeatSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25874),
			new Offset<byte>(groupName, 25875)
		};
		ICE_annunVALVE_OPEN = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25876),
			new Offset<byte>(groupName, 25877)
		};
		ICE_annunCOWL_ANTI_ICE = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25878),
			new Offset<byte>(groupName, 25879)
		};
		ICE_annunCOWL_VALVE_OPEN = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25880),
			new Offset<byte>(groupName, 25881)
		};
		ICE_WingAntiIceSw = new Offset<byte>(groupName, 25882);
		ICE_EngAntiIceSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25883),
			new Offset<byte>(groupName, 25884)
		};
		ICE_WindowHeatTestSw = new Offset<int>(groupName, 25888);
		HYD_annunLOW_PRESS_eng = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25892),
			new Offset<byte>(groupName, 25893)
		};
		HYD_annunLOW_PRESS_elec = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25894),
			new Offset<byte>(groupName, 25895)
		};
		HYD_annunOVERHEAT_elec = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25896),
			new Offset<byte>(groupName, 25897)
		};
		HYD_PumpSw_eng = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25898),
			new Offset<byte>(groupName, 25899)
		};
		HYD_PumpSw_elec = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25900),
			new Offset<byte>(groupName, 25901)
		};
		AIR_TempSourceSelector = new Offset<byte>(groupName, 25902);
		AIR_TrimAirSwitch = new Offset<byte>(groupName, 25903);
		AIR_annunZoneTemp = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 25904),
			new Offset<byte>(groupName, 25905),
			new Offset<byte>(groupName, 25906)
		};
		AIR_annunDualBleed = new Offset<byte>(groupName, 25907);
		AIR_annunRamDoorL = new Offset<byte>(groupName, 25908);
		AIR_annunRamDoorR = new Offset<byte>(groupName, 25909);
		AIR_RecircFanSwitch = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25910),
			new Offset<byte>(groupName, 25911)
		};
		AIR_PackSwitch = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25912),
			new Offset<byte>(groupName, 25913)
		};
		AIR_BleedAirSwitch = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25914),
			new Offset<byte>(groupName, 25915)
		};
		AIR_APUBleedAirSwitch = new Offset<byte>(groupName, 25916);
		AIR_IsolationValveSwitch = new Offset<byte>(groupName, 25917);
		AIR_annunPackTripOff = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25918),
			new Offset<byte>(groupName, 25919)
		};
		AIR_annunWingBodyOverheat = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25920),
			new Offset<byte>(groupName, 25921)
		};
		AIR_annunBleedTripOff = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25922),
			new Offset<byte>(groupName, 25923)
		};
		AIR_annunAUTO_FAIL = new Offset<byte>(groupName, 25924);
		AIR_annunOFFSCHED_DESCENT = new Offset<byte>(groupName, 25925);
		AIR_annunALTN = new Offset<byte>(groupName, 25926);
		AIR_annunMANUAL = new Offset<byte>(groupName, 25927);
		AIR_DuctPress = new Offset<float>[2]
		{
			new Offset<float>(groupName, 25928),
			new Offset<float>(groupName, 25932)
		};
		AIR_DuctPressNeedle = new Offset<float>[2]
		{
			new Offset<float>(groupName, 25936),
			new Offset<float>(groupName, 25940)
		};
		AIR_CabinAltNeedle = new Offset<float>(groupName, 25944);
		AIR_CabinDPNeedle = new Offset<float>(groupName, 25948);
		AIR_CabinVSNeedle = new Offset<float>(groupName, 25952);
		AIR_CabinValveNeedle = new Offset<float>(groupName, 25956);
		AIR_TemperatureNeedle = new Offset<float>(groupName, 25960);
		AIR_DisplayFltAlt = new Offset<string>(groupName, 25964, 6);
		AIR_DisplayLandAlt = new Offset<string>(groupName, 25970, 6);
		DOOR_annunFWD_ENTRY = new Offset<byte>(groupName, 25976);
		DOOR_annunFWD_SERVICE = new Offset<byte>(groupName, 25977);
		DOOR_annunAIRSTAIR = new Offset<byte>(groupName, 25978);
		DOOR_annunLEFT_FWD_OVERWING = new Offset<byte>(groupName, 25979);
		DOOR_annunRIGHT_FWD_OVERWING = new Offset<byte>(groupName, 25980);
		DOOR_annunFWD_CARGO = new Offset<byte>(groupName, 25981);
		DOOR_annunEQUIP = new Offset<byte>(groupName, 25982);
		DOOR_annunLEFT_AFT_OVERWING = new Offset<byte>(groupName, 25983);
		DOOR_annunRIGHT_AFT_OVERWING = new Offset<byte>(groupName, 25984);
		DOOR_annunAFT_CARGO = new Offset<byte>(groupName, 25985);
		DOOR_annunAFT_ENTRY = new Offset<byte>(groupName, 25986);
		DOOR_annunAFT_SERVICE = new Offset<byte>(groupName, 25987);
		AIR_FltAltWindow = new Offset<uint>(groupName, 25988);
		AIR_LandAltWindow = new Offset<uint>(groupName, 25992);
		AIR_OutflowValveSwitch = new Offset<uint>(groupName, 25996);
		AIR_PressurizationModeSelector = new Offset<uint>(groupName, 26000);
		LTS_LandingLtRetractableSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26004),
			new Offset<byte>(groupName, 26005)
		};
		LTS_LandingLtFixedSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26006),
			new Offset<byte>(groupName, 26007)
		};
		LTS_RunwayTurnoffSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26008),
			new Offset<byte>(groupName, 26009)
		};
		LTS_TaxiSw = new Offset<byte>(groupName, 26010);
		APU_Selector = new Offset<byte>(groupName, 26011);
		ENG_StartSelector = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26012),
			new Offset<byte>(groupName, 26013)
		};
		ENG_IgnitionSelector = new Offset<byte>(groupName, 26014);
		LTS_LogoSw = new Offset<byte>(groupName, 26015);
		LTS_PositionSw = new Offset<byte>(groupName, 26016);
		LTS_AntiCollisionSw = new Offset<byte>(groupName, 26017);
		LTS_WingSw = new Offset<byte>(groupName, 26018);
		LTS_WheelWellSw = new Offset<byte>(groupName, 26019);
		WARN_annunFIRE_WARN = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26020),
			new Offset<byte>(groupName, 26021)
		};
		WARN_annunMASTER_CAUTION = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26022),
			new Offset<byte>(groupName, 26023)
		};
		WARN_annunFLT_CONT = new Offset<byte>(groupName, 26024);
		WARN_annunIRS = new Offset<byte>(groupName, 26025);
		WARN_annunFUEL = new Offset<byte>(groupName, 26026);
		WARN_annunELEC = new Offset<byte>(groupName, 26027);
		WARN_annunAPU = new Offset<byte>(groupName, 26028);
		WARN_annunOVHT_DET = new Offset<byte>(groupName, 26029);
		WARN_annunANTI_ICE = new Offset<byte>(groupName, 26030);
		WARN_annunHYD = new Offset<byte>(groupName, 26031);
		WARN_annunDOORS = new Offset<byte>(groupName, 26032);
		WARN_annunENG = new Offset<byte>(groupName, 26033);
		WARN_annunOVERHEAD = new Offset<byte>(groupName, 26034);
		WARN_annunAIR_COND = new Offset<byte>(groupName, 26035);
		EFIS_MinsSelBARO = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26036),
			new Offset<byte>(groupName, 26037)
		};
		EFIS_BaroSelHPA = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26038),
			new Offset<byte>(groupName, 26039)
		};
		EFIS_VORADFSel1 = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26040),
			new Offset<byte>(groupName, 26041)
		};
		EFIS_VORADFSel2 = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26042),
			new Offset<byte>(groupName, 26043)
		};
		EFIS_ModeSel = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26044),
			new Offset<byte>(groupName, 26045)
		};
		EFIS_RangeSel = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26046),
			new Offset<byte>(groupName, 26047)
		};
		MCP_Course = new Offset<ushort>[2]
		{
			new Offset<ushort>(groupName, 26048),
			new Offset<ushort>(groupName, 26050)
		};
		MCP_IASMach = new Offset<float>(groupName, 26052);
		MCP_IASBlank = new Offset<byte>(groupName, 26056);
		MCP_IASOverspeedFlash = new Offset<byte>(groupName, 26057);
		MCP_IASUnderspeedFlash = new Offset<byte>(groupName, 26058);
		MCP_Heading = new Offset<ushort>(groupName, 26060);
		MCP_Altitude = new Offset<ushort>(groupName, 26062);
		MCP_VertSpeed = new Offset<ushort>(groupName, 26064);
		MCP_VertSpeedBlank = new Offset<byte>(groupName, 26066);
		MCP_FDSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26067),
			new Offset<byte>(groupName, 26068)
		};
		MCP_ATArmSw = new Offset<byte>(groupName, 26069);
		MCP_BankLimitSel = new Offset<byte>(groupName, 26070);
		MCP_DisengageBar = new Offset<byte>(groupName, 26071);
		MCP_annunFD = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26072),
			new Offset<byte>(groupName, 26073)
		};
		MCP_annunATArm = new Offset<byte>(groupName, 26074);
		MCP_annunN1 = new Offset<byte>(groupName, 26075);
		MCP_annunSPEED = new Offset<byte>(groupName, 26076);
		MCP_annunVNAV = new Offset<byte>(groupName, 26077);
		MCP_annunLVL_CHG = new Offset<byte>(groupName, 26078);
		MCP_annunHDG_SEL = new Offset<byte>(groupName, 26079);
		MCP_annunLNAV = new Offset<byte>(groupName, 26080);
		MCP_annunVOR_LOC = new Offset<byte>(groupName, 26081);
		MCP_annunAPP = new Offset<byte>(groupName, 26082);
		MCP_annunALT_HOLD = new Offset<byte>(groupName, 26083);
		MCP_annunVS = new Offset<byte>(groupName, 26084);
		MCP_annunCMD_A = new Offset<byte>(groupName, 26085);
		MCP_annunCWS_A = new Offset<byte>(groupName, 26086);
		MCP_annunCMD_B = new Offset<byte>(groupName, 26087);
		MCP_annunCWS_B = new Offset<byte>(groupName, 26088);
		MCP_indication_powered = new Offset<byte>(groupName, 26089);
		MAIN_NoseWheelSteeringSwNORM = new Offset<byte>(groupName, 26090);
		MAIN_annunBELOW_GS = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26091),
			new Offset<byte>(groupName, 26092)
		};
		MAIN_MainPanelDUSel = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26093),
			new Offset<byte>(groupName, 26094)
		};
		MAIN_LowerDUSel = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26095),
			new Offset<byte>(groupName, 26096)
		};
		MAIN_annunAP = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26097),
			new Offset<byte>(groupName, 26098)
		};
		MAIN_annunAP_Amber = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26099),
			new Offset<byte>(groupName, 26100)
		};
		MAIN_annunAT = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26101),
			new Offset<byte>(groupName, 26102)
		};
		MAIN_annunAT_Amber = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26103),
			new Offset<byte>(groupName, 26104)
		};
		MAIN_annunFMC = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26105),
			new Offset<byte>(groupName, 26106)
		};
		MAIN_DisengageTestSelector = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26107),
			new Offset<byte>(groupName, 26108)
		};
		MAIN_annunSPEEDBRAKE_ARMED = new Offset<byte>(groupName, 26109);
		MAIN_annunSPEEDBRAKE_DO_NOT_ARM = new Offset<byte>(groupName, 26110);
		MAIN_annunSPEEDBRAKE_EXTENDED = new Offset<byte>(groupName, 26111);
		MAIN_annunSTAB_OUT_OF_TRIM = new Offset<byte>(groupName, 26112);
		MAIN_LightsSelector = new Offset<byte>(groupName, 26113);
		MAIN_RMISelector1_VOR = new Offset<byte>(groupName, 26114);
		MAIN_RMISelector2_VOR = new Offset<byte>(groupName, 26115);
		MAIN_N1SetSelector = new Offset<byte>(groupName, 26116);
		MAIN_SpdRefSelector = new Offset<byte>(groupName, 26117);
		MAIN_FuelFlowSelector = new Offset<byte>(groupName, 26118);
		MAIN_AutobrakeSelector = new Offset<byte>(groupName, 26119);
		MAIN_annunANTI_SKID_INOP = new Offset<byte>(groupName, 26120);
		MAIN_annunAUTO_BRAKE_DISARM = new Offset<byte>(groupName, 26121);
		MAIN_annunLE_FLAPS_TRANSIT = new Offset<byte>(groupName, 26122);
		MAIN_annunLE_FLAPS_EXT = new Offset<byte>(groupName, 26123);
		MAIN_TEFlapsNeedle = new Offset<float>[2]
		{
			new Offset<float>(groupName, 26124),
			new Offset<float>(groupName, 26128)
		};
		MAIN_annunGEAR_transit = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 26132),
			new Offset<byte>(groupName, 26133),
			new Offset<byte>(groupName, 26134)
		};
		MAIN_annunGEAR_locked = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 26135),
			new Offset<byte>(groupName, 26136),
			new Offset<byte>(groupName, 26137)
		};
		MAIN_GearLever = new Offset<byte>(groupName, 26138);
		MAIN_BrakePressNeedle = new Offset<float>(groupName, 26140);
		MAIN_annunCABIN_ALTITUDE = new Offset<byte>(groupName, 27648);
		MAIN_annunTAKEOFF_CONFIG = new Offset<byte>(groupName, 27649);
		HGS_annun_AIII = new Offset<byte>(groupName, 27650);
		HGS_annun_NO_AIII = new Offset<byte>(groupName, 27651);
		HGS_annun_FLARE = new Offset<byte>(groupName, 27652);
		HGS_annun_RO = new Offset<byte>(groupName, 27653);
		HGS_annun_RO_CTN = new Offset<byte>(groupName, 27654);
		HGS_annun_RO_ARM = new Offset<byte>(groupName, 27655);
		HGS_annun_TO = new Offset<byte>(groupName, 27656);
		HGS_annun_TO_CTN = new Offset<byte>(groupName, 27657);
		HGS_annun_APCH = new Offset<byte>(groupName, 27658);
		HGS_annun_TO_WARN = new Offset<byte>(groupName, 27659);
		HGS_annun_Bar = new Offset<byte>(groupName, 27660);
		HGS_annun_FAIL = new Offset<byte>(groupName, 27661);
		LTS_MainPanelKnob = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27662),
			new Offset<byte>(groupName, 27663)
		};
		LTS_BackgroundKnob = new Offset<byte>(groupName, 27664);
		LTS_AFDSFloodKnob = new Offset<byte>(groupName, 27665);
		LTS_OutbdDUBrtKnob = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27666),
			new Offset<byte>(groupName, 27667)
		};
		LTS_InbdDUBrtKnob = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27668),
			new Offset<byte>(groupName, 27669)
		};
		LTS_InbdDUMapBrtKnob = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27670),
			new Offset<byte>(groupName, 27671)
		};
		LTS_UpperDUBrtKnob = new Offset<byte>(groupName, 27672);
		LTS_LowerDUBrtKnob = new Offset<byte>(groupName, 27673);
		LTS_LowerDUMapBrtKnob = new Offset<byte>(groupName, 27674);
		GPWS_annunINOP = new Offset<byte>(groupName, 27675);
		GPWS_FlapInhibitSw_NORM = new Offset<byte>(groupName, 27676);
		GPWS_GearInhibitSw_NORM = new Offset<byte>(groupName, 27677);
		GPWS_TerrInhibitSw_NORM = new Offset<byte>(groupName, 27678);
		CDU_annunEXEC = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27679),
			new Offset<byte>(groupName, 27680)
		};
		CDU_annunCALL = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27681),
			new Offset<byte>(groupName, 27682)
		};
		CDU_annunFAIL = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27683),
			new Offset<byte>(groupName, 27684)
		};
		CDU_annunMSG = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27685),
			new Offset<byte>(groupName, 27686)
		};
		CDU_annunOFST = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27687),
			new Offset<byte>(groupName, 27688)
		};
		CDU_BrtKnob = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27689),
			new Offset<byte>(groupName, 27690)
		};
		COMM_Attend_PressCount = new Offset<byte>(groupName, 27691);
		COMM_GrdCall_PressCount = new Offset<byte>(groupName, 27692);
		COMM_SelectedMic = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 27693),
			new Offset<byte>(groupName, 27694),
			new Offset<byte>(groupName, 27695)
		};
		COMM_ReceiverSwitches = new Offset<uint>[3]
		{
			new Offset<uint>(groupName, 27696),
			new Offset<uint>(groupName, 27700),
			new Offset<uint>(groupName, 27704)
		};
		TRIM_StabTrimMainElecSw_NORMAL = new Offset<byte>(groupName, 27708);
		TRIM_StabTrimAutoPilotSw_NORMAL = new Offset<byte>(groupName, 27709);
		PED_annunParkingBrake = new Offset<byte>(groupName, 27710);
		FIRE_OvhtDetSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27711),
			new Offset<byte>(groupName, 27712)
		};
		FIRE_annunENG_OVERHEAT = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27713),
			new Offset<byte>(groupName, 27714)
		};
		FIRE_DetTestSw = new Offset<byte>(groupName, 27715);
		FIRE_HandlePos = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 27716),
			new Offset<byte>(groupName, 27717),
			new Offset<byte>(groupName, 27718)
		};
		FIRE_HandleIlluminated = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 27719),
			new Offset<byte>(groupName, 27720),
			new Offset<byte>(groupName, 27721)
		};
		FIRE_annunWHEEL_WELL = new Offset<byte>(groupName, 27722);
		FIRE_annunFAULT = new Offset<byte>(groupName, 27723);
		FIRE_annunAPU_DET_INOP = new Offset<byte>(groupName, 27724);
		FIRE_annunAPU_BOTTLE_DISCHARGE = new Offset<byte>(groupName, 27725);
		FIRE_annunBOTTLE_DISCHARGE = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27726),
			new Offset<byte>(groupName, 27727)
		};
		FIRE_ExtinguisherTestSw = new Offset<byte>(groupName, 27728);
		FIRE_annunExtinguisherTest = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 27729),
			new Offset<byte>(groupName, 27730),
			new Offset<byte>(groupName, 27731)
		};
		CARGO_annunExtTest = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27732),
			new Offset<byte>(groupName, 27733)
		};
		CARGO_DetSelect = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27734),
			new Offset<byte>(groupName, 27735)
		};
		CARGO_ArmedSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27736),
			new Offset<byte>(groupName, 27737)
		};
		CARGO_annunFWD = new Offset<byte>(groupName, 27738);
		CARGO_annunAFT = new Offset<byte>(groupName, 27739);
		CARGO_annunDETECTOR_FAULT = new Offset<byte>(groupName, 27740);
		CARGO_annunDISCH = new Offset<byte>(groupName, 27741);
		HGS_annunRWY = new Offset<byte>(groupName, 27742);
		HGS_annunGS = new Offset<byte>(groupName, 27743);
		HGS_annunFAULT = new Offset<byte>(groupName, 27744);
		HGS_annunCLR = new Offset<byte>(groupName, 27745);
		XPDR_XpndrSelector_2 = new Offset<byte>(groupName, 27746);
		XPDR_AltSourceSel_2 = new Offset<byte>(groupName, 27747);
		XPDR_ModeSel = new Offset<byte>(groupName, 27748);
		XPDR_annunFAIL = new Offset<byte>(groupName, 27749);
		LTS_PedFloodKnob = new Offset<byte>(groupName, 27750);
		LTS_PedPanelKnob = new Offset<byte>(groupName, 27751);
		TRIM_StabTrimSw_NORMAL = new Offset<byte>(groupName, 27752);
		PED_annunLOCK_FAIL = new Offset<byte>(groupName, 27753);
		PED_annunAUTO_UNLK = new Offset<byte>(groupName, 27754);
		PED_FltDkDoorSel = new Offset<byte>(groupName, 27755);
		FMC_TakeoffFlaps = new Offset<byte>(groupName, 27756);
		FMC_V1 = new Offset<byte>(groupName, 27757);
		FMC_VR = new Offset<byte>(groupName, 27758);
		FMC_V2 = new Offset<byte>(groupName, 27759);
		FMC_LandingFlaps = new Offset<byte>(groupName, 27760);
		FMC_LandingVREF = new Offset<byte>(groupName, 27761);
		FMC_CruiseAlt = new Offset<ushort>(groupName, 27762);
		FMC_LandingAltitude = new Offset<ushort>(groupName, 27764);
		FMC_TransitionAlt = new Offset<ushort>(groupName, 27766);
		FMC_TransitionLevel = new Offset<ushort>(groupName, 27768);
		FMC_PerfInputComplete = new Offset<byte>(groupName, 27770);
		FMC_DistanceToTOD = new Offset<float>(groupName, 27772);
		FMC_DistanceToDest = new Offset<float>(groupName, 27776);
		FMC_flightNumber = new Offset<string>(groupName, 27780, 9);
		AircraftModel = new Offset<byte>(groupName, 27789);
		WeightInKg = new Offset<byte>(groupName, 27790);
		GPWS_V1CallEnabled = new Offset<byte>(groupName, 27791);
		GroundConnAvailable = new Offset<byte>(groupName, 27792);
	}

	private void instatiate_FSX_P3D_Offsets()
	{
		IRS_DisplaySelector = new Offset<byte>(groupName, 25632);
		IRS_SysDisplay_R = new Offset<byte>(groupName, 25633);
		IRS_annunGPS = new Offset<byte>(groupName, 25634);
		IRS_annunALIGN = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25635),
			new Offset<byte>(groupName, 25636)
		};
		IRS_annunON_DC = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25637),
			new Offset<byte>(groupName, 25638)
		};
		IRS_annunFAULT = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25639),
			new Offset<byte>(groupName, 25640)
		};
		IRS_annunDC_FAIL = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25641),
			new Offset<byte>(groupName, 25642)
		};
		IRS_ModeSelector = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25643),
			new Offset<byte>(groupName, 25644)
		};
		WARN_annunPSEU = new Offset<byte>(groupName, 25645);
		COMM_ServiceInterphoneSw = new Offset<byte>(groupName, 25646);
		LTS_DomeWhiteSw = new Offset<byte>(groupName, 25647);
		ENG_EECSwitch = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25648),
			new Offset<byte>(groupName, 25649)
		};
		ENG_annunREVERSER = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25650),
			new Offset<byte>(groupName, 25651)
		};
		ENG_annunENGINE_CONTROL = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25652),
			new Offset<byte>(groupName, 25653)
		};
		ENG_annunALTN = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25654),
			new Offset<byte>(groupName, 25655)
		};
		OXY_Needle = new Offset<byte>(groupName, 25656);
		OXY_SwNormal = new Offset<byte>(groupName, 25657);
		OXY_annunPASS_OXY_ON = new Offset<byte>(groupName, 25658);
		GEAR_annunOvhdLEFT = new Offset<byte>(groupName, 25659);
		GEAR_annunOvhdNOSE = new Offset<byte>(groupName, 25660);
		GEAR_annunOvhdRIGHT = new Offset<byte>(groupName, 25661);
		FLTREC_SwNormal = new Offset<byte>(groupName, 25662);
		FLTREC_annunOFF = new Offset<byte>(groupName, 25663);
		FCTL_FltControl_Sw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25664),
			new Offset<byte>(groupName, 25665)
		};
		FCTL_Spoiler_Sw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25666),
			new Offset<byte>(groupName, 25667)
		};
		FCTL_YawDamper_Sw = new Offset<byte>(groupName, 25668);
		FCTL_AltnFlaps_Sw_ARM = new Offset<byte>(groupName, 25669);
		FCTL_AltnFlaps_Control_Sw = new Offset<byte>(groupName, 25670);
		FCTL_annunFC_LOW_PRESSURE = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25671),
			new Offset<byte>(groupName, 25672)
		};
		FCTL_annunYAW_DAMPER = new Offset<byte>(groupName, 25673);
		FCTL_annunLOW_QUANTITY = new Offset<byte>(groupName, 25674);
		FCTL_annunLOW_PRESSURE = new Offset<byte>(groupName, 25675);
		FCTL_annunLOW_STBY_RUD_ON = new Offset<byte>(groupName, 25676);
		FCTL_annunFEEL_DIFF_PRESS = new Offset<byte>(groupName, 25677);
		FCTL_annunSPEED_TRIM_FAIL = new Offset<byte>(groupName, 25678);
		FCTL_annunMACH_TRIM_FAIL = new Offset<byte>(groupName, 25679);
		FCTL_annunAUTO_SLAT_FAIL = new Offset<byte>(groupName, 25680);
		NAVDIS_VHFNavSelector = new Offset<byte>(groupName, 25681);
		NAVDIS_IRSSelector = new Offset<byte>(groupName, 25682);
		NAVDIS_FMCSelector = new Offset<byte>(groupName, 25683);
		NAVDIS_SourceSelector = new Offset<byte>(groupName, 25684);
		NAVDIS_ControlPaneSelector = new Offset<byte>(groupName, 25685);
		FUEL_FuelTempNeedle = new Offset<float>(groupName, 25688);
		FUEL_CrossFeedSw = new Offset<byte>(groupName, 25692);
		FUEL_PumpFwdSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25693),
			new Offset<byte>(groupName, 25694)
		};
		FUEL_PumpAftSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25695),
			new Offset<byte>(groupName, 25696)
		};
		FUEL_PumpCtrSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25697),
			new Offset<byte>(groupName, 25698)
		};
		FUEL_annunENG_VALVE_CLOSED = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25699),
			new Offset<byte>(groupName, 25700)
		};
		FUEL_annunSPAR_VALVE_CLOSED = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25701),
			new Offset<byte>(groupName, 25702)
		};
		FUEL_annunFILTER_BYPASS = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25703),
			new Offset<byte>(groupName, 25704)
		};
		FUEL_annunXFEED_VALVE_OPEN = new Offset<byte>(groupName, 25705);
		FUEL_annunLOWPRESS_Fwd = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25706),
			new Offset<byte>(groupName, 25707)
		};
		FUEL_annunLOWPRESS_Aft = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25708),
			new Offset<byte>(groupName, 25709)
		};
		FUEL_annunLOWPRESS_Ctr = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25710),
			new Offset<byte>(groupName, 25711)
		};
		ELEC_annunBAT_DISCHARGE = new Offset<byte>(groupName, 25712);
		ELEC_annunTR_UNIT = new Offset<byte>(groupName, 25713);
		ELEC_annunELEC = new Offset<byte>(groupName, 25714);
		ELEC_DCMeterSelector = new Offset<byte>(groupName, 25715);
		ELEC_ACMeterSelector = new Offset<byte>(groupName, 25716);
		ELEC_BatSelector = new Offset<byte>(groupName, 25717);
		ELEC_CabUtilSw = new Offset<byte>(groupName, 25718);
		ELEC_IFEPassSeatSw = new Offset<byte>(groupName, 25719);
		ELEC_annunDRIVE = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25720),
			new Offset<byte>(groupName, 25721)
		};
		ELEC_annunSTANDBY_POWER_OFF = new Offset<byte>(groupName, 25722);
		ELEC_IDGDisconnectSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25723),
			new Offset<byte>(groupName, 25724)
		};
		ELEC_StandbyPowerSelector = new Offset<byte>(groupName, 25725);
		ELEC_annunGRD_POWER_AVAILABLE = new Offset<byte>(groupName, 25726);
		ELEC_GrdPwrSw = new Offset<byte>(groupName, 25727);
		ELEC_BusTransSw_AUTO = new Offset<byte>(groupName, 25728);
		ELEC_GenSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25729),
			new Offset<byte>(groupName, 25730)
		};
		ELEC_APUGenSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25731),
			new Offset<byte>(groupName, 25732)
		};
		ELEC_annunTRANSFER_BUS_OFF = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25733),
			new Offset<byte>(groupName, 25734)
		};
		ELEC_annunSOURCE_OFF = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25735),
			new Offset<byte>(groupName, 25736)
		};
		ELEC_annunGEN_BUS_OFF = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25737),
			new Offset<byte>(groupName, 25738)
		};
		ELEC_annunAPU_GEN_OFF_BUS = new Offset<byte>(groupName, 25739);
		APU_EGTNeedle = new Offset<float>(groupName, 25740);
		APU_annunMAINT = new Offset<byte>(groupName, 25744);
		APU_annunLOW_OIL_PRESSURE = new Offset<byte>(groupName, 25745);
		APU_annunFAULT = new Offset<byte>(groupName, 25746);
		APU_annunOVERSPEED = new Offset<byte>(groupName, 25747);
		OH_WiperLSelector = new Offset<byte>(groupName, 25748);
		OH_WiperRSelector = new Offset<byte>(groupName, 25749);
		LTS_CircuitBreakerKnob = new Offset<byte>(groupName, 25750);
		LTS_OvereadPanelKnob = new Offset<byte>(groupName, 25751);
		AIR_EquipCoolingSupplyNORM = new Offset<byte>(groupName, 25752);
		AIR_EquipCoolingExhaustNORM = new Offset<byte>(groupName, 25753);
		AIR_annunEquipCoolingSupplyOFF = new Offset<byte>(groupName, 25754);
		AIR_annunEquipCoolingExhaustOFF = new Offset<byte>(groupName, 25755);
		LTS_annunEmerNOT_ARMED = new Offset<byte>(groupName, 25756);
		LTS_EmerExitSelector = new Offset<byte>(groupName, 25757);
		COMM_NoSmokingSelector = new Offset<byte>(groupName, 25758);
		COMM_FastenBeltsSelector = new Offset<byte>(groupName, 25759);
		COMM_annunCALL = new Offset<byte>(groupName, 25760);
		COMM_annunPA_IN_USE = new Offset<byte>(groupName, 25761);
		ICE_annunOVERHEAT = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25762),
			new Offset<byte>(groupName, 25763),
			new Offset<byte>(groupName, 25764),
			new Offset<byte>(groupName, 25765)
		};
		ICE_annunON = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25766),
			new Offset<byte>(groupName, 25767),
			new Offset<byte>(groupName, 25768),
			new Offset<byte>(groupName, 25769)
		};
		ICE_WindowHeatSw = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25770),
			new Offset<byte>(groupName, 25771),
			new Offset<byte>(groupName, 25772),
			new Offset<byte>(groupName, 25773)
		};
		ICE_annunCAPT_PITOT = new Offset<byte>(groupName, 25774);
		ICE_annunL_ELEV_PITOT = new Offset<byte>(groupName, 25775);
		ICE_annunL_ALPHA_VANE = new Offset<byte>(groupName, 25776);
		ICE_annunL_TEMP_PROBE = new Offset<byte>(groupName, 25777);
		ICE_annunFO_PITOT = new Offset<byte>(groupName, 25778);
		ICE_annunR_ELEV_PITOT = new Offset<byte>(groupName, 25779);
		ICE_annunR_ALPHA_VANE = new Offset<byte>(groupName, 25780);
		ICE_annunAUX_PITOT = new Offset<byte>(groupName, 25781);
		ICE_TestProbeHeatSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25782),
			new Offset<byte>(groupName, 25783)
		};
		ICE_annunVALVE_OPEN = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25784),
			new Offset<byte>(groupName, 25785)
		};
		ICE_annunCOWL_ANTI_ICE = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25786),
			new Offset<byte>(groupName, 25787)
		};
		ICE_annunCOWL_VALVE_OPEN = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25788),
			new Offset<byte>(groupName, 25789)
		};
		ICE_WingAntiIceSw = new Offset<byte>(groupName, 25790);
		ICE_EngAntiIceSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25791),
			new Offset<byte>(groupName, 25792)
		};
		HYD_annunLOW_PRESS_eng = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25793),
			new Offset<byte>(groupName, 25794)
		};
		HYD_annunLOW_PRESS_elec = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25795),
			new Offset<byte>(groupName, 25796)
		};
		HYD_annunOVERHEAT_elec = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25797),
			new Offset<byte>(groupName, 25798)
		};
		HYD_PumpSw_eng = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25799),
			new Offset<byte>(groupName, 25800)
		};
		HYD_PumpSw_elec = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25801),
			new Offset<byte>(groupName, 25802)
		};
		AIR_TempSourceSelector = new Offset<byte>(groupName, 25803);
		AIR_TrimAirSwitch = new Offset<byte>(groupName, 25804);
		AIR_annunZoneTemp = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 25805),
			new Offset<byte>(groupName, 25806),
			new Offset<byte>(groupName, 25807)
		};
		AIR_annunDualBleed = new Offset<byte>(groupName, 25808);
		AIR_annunRamDoorL = new Offset<byte>(groupName, 25809);
		AIR_annunRamDoorR = new Offset<byte>(groupName, 25810);
		AIR_RecircFanSwitch = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25811),
			new Offset<byte>(groupName, 25812)
		};
		AIR_PackSwitch = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25813),
			new Offset<byte>(groupName, 25814)
		};
		AIR_BleedAirSwitch = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25815),
			new Offset<byte>(groupName, 25816)
		};
		AIR_APUBleedAirSwitch = new Offset<byte>(groupName, 25817);
		AIR_IsolationValveSwitch = new Offset<byte>(groupName, 25818);
		AIR_annunPackTripOff = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25819),
			new Offset<byte>(groupName, 25820)
		};
		AIR_annunWingBodyOverheat = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25821),
			new Offset<byte>(groupName, 25822)
		};
		AIR_annunBleedTripOff = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25823),
			new Offset<byte>(groupName, 25824)
		};
		AIR_FltAltWindow = new Offset<uint>(groupName, 25828);
		AIR_LandAltWindow = new Offset<uint>(groupName, 25832);
		AIR_OutflowValveSwitch = new Offset<uint>(groupName, 25836);
		AIR_PressurizationModeSelector = new Offset<uint>(groupName, 25840);
		LTS_LandingLtRetractableSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25844),
			new Offset<byte>(groupName, 25845)
		};
		LTS_LandingLtFixedSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25846),
			new Offset<byte>(groupName, 25847)
		};
		LTS_RunwayTurnoffSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25848),
			new Offset<byte>(groupName, 25849)
		};
		LTS_TaxiSw = new Offset<byte>(groupName, 25850);
		APU_Selector = new Offset<byte>(groupName, 25851);
		ENG_StartSelector = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25852),
			new Offset<byte>(groupName, 25853)
		};
		ENG_IgnitionSelector = new Offset<byte>(groupName, 25854);
		LTS_LogoSw = new Offset<byte>(groupName, 25855);
		LTS_PositionSw = new Offset<byte>(groupName, 25856);
		LTS_AntiCollisionSw = new Offset<byte>(groupName, 25857);
		LTS_WingSw = new Offset<byte>(groupName, 25858);
		LTS_WheelWellSw = new Offset<byte>(groupName, 25859);
		WARN_annunFIRE_WARN = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25860),
			new Offset<byte>(groupName, 25861)
		};
		WARN_annunMASTER_CAUTION = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25862),
			new Offset<byte>(groupName, 25863)
		};
		WARN_annunFLT_CONT = new Offset<byte>(groupName, 25864);
		WARN_annunIRS = new Offset<byte>(groupName, 25865);
		WARN_annunFUEL = new Offset<byte>(groupName, 25866);
		WARN_annunELEC = new Offset<byte>(groupName, 25867);
		WARN_annunAPU = new Offset<byte>(groupName, 25868);
		WARN_annunOVHT_DET = new Offset<byte>(groupName, 25869);
		WARN_annunANTI_ICE = new Offset<byte>(groupName, 25870);
		WARN_annunHYD = new Offset<byte>(groupName, 25871);
		WARN_annunDOORS = new Offset<byte>(groupName, 25872);
		WARN_annunENG = new Offset<byte>(groupName, 25873);
		WARN_annunOVERHEAD = new Offset<byte>(groupName, 25874);
		WARN_annunAIR_COND = new Offset<byte>(groupName, 25875);
		EFIS_MinsSelBARO = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25876),
			new Offset<byte>(groupName, 25877)
		};
		EFIS_BaroSelHPA = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25878),
			new Offset<byte>(groupName, 25879)
		};
		EFIS_VORADFSel1 = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25880),
			new Offset<byte>(groupName, 25881)
		};
		EFIS_VORADFSel2 = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25882),
			new Offset<byte>(groupName, 25883)
		};
		EFIS_ModeSel = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25884),
			new Offset<byte>(groupName, 25885)
		};
		EFIS_RangeSel = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25886),
			new Offset<byte>(groupName, 25887)
		};
		MCP_Course = new Offset<ushort>[2]
		{
			new Offset<ushort>(groupName, 25888),
			new Offset<ushort>(groupName, 25890)
		};
		MCP_IASMach = new Offset<float>(groupName, 25892);
		MCP_IASBlank = new Offset<byte>(groupName, 25896);
		MCP_IASOverspeedFlash = new Offset<byte>(groupName, 25897);
		MCP_IASUnderspeedFlash = new Offset<byte>(groupName, 25898);
		MCP_Heading = new Offset<ushort>(groupName, 25900);
		MCP_Altitude = new Offset<ushort>(groupName, 25902);
		MCP_VertSpeed = new Offset<ushort>(groupName, 25904);
		MCP_VertSpeedBlank = new Offset<byte>(groupName, 25906);
		MCP_FDSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25907),
			new Offset<byte>(groupName, 25908)
		};
		MCP_ATArmSw = new Offset<byte>(groupName, 25909);
		MCP_BankLimitSel = new Offset<byte>(groupName, 25910);
		MCP_DisengageBar = new Offset<byte>(groupName, 25911);
		MCP_annunFD = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25912),
			new Offset<byte>(groupName, 25913)
		};
		MCP_annunATArm = new Offset<byte>(groupName, 25914);
		MCP_annunN1 = new Offset<byte>(groupName, 25915);
		MCP_annunSPEED = new Offset<byte>(groupName, 25916);
		MCP_annunVNAV = new Offset<byte>(groupName, 25917);
		MCP_annunLVL_CHG = new Offset<byte>(groupName, 25918);
		MCP_annunHDG_SEL = new Offset<byte>(groupName, 25919);
		MCP_annunLNAV = new Offset<byte>(groupName, 25920);
		MCP_annunVOR_LOC = new Offset<byte>(groupName, 25921);
		MCP_annunAPP = new Offset<byte>(groupName, 25922);
		MCP_annunALT_HOLD = new Offset<byte>(groupName, 25923);
		MCP_annunVS = new Offset<byte>(groupName, 25924);
		MCP_annunCMD_A = new Offset<byte>(groupName, 25925);
		MCP_annunCWS_A = new Offset<byte>(groupName, 25926);
		MCP_annunCMD_B = new Offset<byte>(groupName, 25927);
		MCP_annunCWS_B = new Offset<byte>(groupName, 25928);
		MAIN_NoseWheelSteeringSwNORM = new Offset<byte>(groupName, 25929);
		MAIN_annunBELOW_GS = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25930),
			new Offset<byte>(groupName, 25931)
		};
		MAIN_MainPanelDUSel = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25932),
			new Offset<byte>(groupName, 25933)
		};
		MAIN_LowerDUSel = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25934),
			new Offset<byte>(groupName, 25935)
		};
		MAIN_annunAP = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25936),
			new Offset<byte>(groupName, 25937)
		};
		MAIN_annunAT = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25938),
			new Offset<byte>(groupName, 25939)
		};
		MAIN_annunFMC = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25940),
			new Offset<byte>(groupName, 25941)
		};
		MAIN_DisengageTestSelector = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25942),
			new Offset<byte>(groupName, 25943)
		};
		MAIN_annunSPEEDBRAKE_ARMED = new Offset<byte>(groupName, 25944);
		MAIN_annunSPEEDBRAKE_DO_NOT_ARM = new Offset<byte>(groupName, 25945);
		MAIN_annunSPEEDBRAKE_EXTENDED = new Offset<byte>(groupName, 25946);
		MAIN_annunSTAB_OUT_OF_TRIM = new Offset<byte>(groupName, 25947);
		MAIN_LightsSelector = new Offset<byte>(groupName, 25948);
		MAIN_RMISelector1_VOR = new Offset<byte>(groupName, 25949);
		MAIN_RMISelector2_VOR = new Offset<byte>(groupName, 25950);
		MAIN_N1SetSelector = new Offset<byte>(groupName, 25951);
		MAIN_SpdRefSelector = new Offset<byte>(groupName, 25952);
		MAIN_FuelFlowSelector = new Offset<byte>(groupName, 25953);
		MAIN_AutobrakeSelector = new Offset<byte>(groupName, 25954);
		MAIN_annunANTI_SKID_INOP = new Offset<byte>(groupName, 25955);
		MAIN_annunAUTO_BRAKE_DISARM = new Offset<byte>(groupName, 25956);
		MAIN_annunLE_FLAPS_TRANSIT = new Offset<byte>(groupName, 25957);
		MAIN_annunLE_FLAPS_EXT = new Offset<byte>(groupName, 25958);
		MAIN_TEFlapsNeedle = new Offset<float>[2]
		{
			new Offset<float>(groupName, 25960),
			new Offset<float>(groupName, 25964)
		};
		MAIN_annunGEAR_transit = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 25968),
			new Offset<byte>(groupName, 25969),
			new Offset<byte>(groupName, 25970)
		};
		MAIN_annunGEAR_locked = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 25971),
			new Offset<byte>(groupName, 25972),
			new Offset<byte>(groupName, 25973)
		};
		MAIN_GearLever = new Offset<byte>(groupName, 25974);
		MAIN_BrakePressNeedle = new Offset<float>(groupName, 25976);
		HGS_annun_AIII = new Offset<byte>(groupName, 25980);
		HGS_annun_NO_AIII = new Offset<byte>(groupName, 25981);
		HGS_annun_FLARE = new Offset<byte>(groupName, 25982);
		HGS_annun_RO = new Offset<byte>(groupName, 25983);
		HGS_annun_RO_CTN = new Offset<byte>(groupName, 25984);
		HGS_annun_RO_ARM = new Offset<byte>(groupName, 25985);
		HGS_annun_TO = new Offset<byte>(groupName, 25986);
		HGS_annun_TO_CTN = new Offset<byte>(groupName, 25987);
		HGS_annun_APCH = new Offset<byte>(groupName, 25988);
		HGS_annun_TO_WARN = new Offset<byte>(groupName, 25989);
		HGS_annun_Bar = new Offset<byte>(groupName, 25990);
		HGS_annun_FAIL = new Offset<byte>(groupName, 25991);
		LTS_MainPanelKnob = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25992),
			new Offset<byte>(groupName, 25993)
		};
		LTS_BackgroundKnob = new Offset<byte>(groupName, 25994);
		LTS_AFDSFloodKnob = new Offset<byte>(groupName, 25995);
		LTS_OutbdDUBrtKnob = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25996),
			new Offset<byte>(groupName, 25997)
		};
		LTS_InbdDUBrtKnob = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25998),
			new Offset<byte>(groupName, 25999)
		};
		LTS_InbdDUMapBrtKnob = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26000),
			new Offset<byte>(groupName, 26001)
		};
		LTS_UpperDUBrtKnob = new Offset<byte>(groupName, 26002);
		LTS_LowerDUBrtKnob = new Offset<byte>(groupName, 26003);
		LTS_LowerDUMapBrtKnob = new Offset<byte>(groupName, 26004);
		GPWS_annunINOP = new Offset<byte>(groupName, 26005);
		GPWS_FlapInhibitSw_NORM = new Offset<byte>(groupName, 26006);
		GPWS_GearInhibitSw_NORM = new Offset<byte>(groupName, 26007);
		GPWS_TerrInhibitSw_NORM = new Offset<byte>(groupName, 26008);
		CDU_annunEXEC = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26009),
			new Offset<byte>(groupName, 26010)
		};
		CDU_annunCALL = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26011),
			new Offset<byte>(groupName, 26012)
		};
		CDU_annunFAIL = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26013),
			new Offset<byte>(groupName, 26014)
		};
		CDU_annunMSG = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26015),
			new Offset<byte>(groupName, 26016)
		};
		CDU_annunOFST = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26017),
			new Offset<byte>(groupName, 26018)
		};
		CDU_BrtKnob = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26019),
			new Offset<byte>(groupName, 26020)
		};
		TRIM_StabTrimMainElecSw_NORMAL = new Offset<byte>(groupName, 26021);
		TRIM_StabTrimAutoPilotSw_NORMAL = new Offset<byte>(groupName, 26022);
		PED_annunParkingBrake = new Offset<byte>(groupName, 26023);
		FIRE_OvhtDetSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26024),
			new Offset<byte>(groupName, 26025)
		};
		FIRE_annunENG_OVERHEAT = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26026),
			new Offset<byte>(groupName, 26027)
		};
		FIRE_DetTestSw = new Offset<byte>(groupName, 26028);
		FIRE_HandlePos = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 26029),
			new Offset<byte>(groupName, 26030),
			new Offset<byte>(groupName, 26031)
		};
		FIRE_HandleIlluminated = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 26032),
			new Offset<byte>(groupName, 26033),
			new Offset<byte>(groupName, 26034)
		};
		FIRE_annunWHEEL_WELL = new Offset<byte>(groupName, 26035);
		FIRE_annunFAULT = new Offset<byte>(groupName, 26036);
		FIRE_annunAPU_DET_INOP = new Offset<byte>(groupName, 26037);
		FIRE_annunAPU_BOTTLE_DISCHARGE = new Offset<byte>(groupName, 26038);
		FIRE_annunBOTTLE_DISCHARGE = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26039),
			new Offset<byte>(groupName, 26040)
		};
		FIRE_ExtinguisherTestSw = new Offset<byte>(groupName, 26041);
		FIRE_annunExtinguisherTest = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 26042),
			new Offset<byte>(groupName, 26043),
			new Offset<byte>(groupName, 26044)
		};
		CARGO_annunExtTest = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26045),
			new Offset<byte>(groupName, 26046)
		};
		CARGO_DetSelect = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26047),
			new Offset<byte>(groupName, 26048)
		};
		CARGO_ArmedSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26049),
			new Offset<byte>(groupName, 26050)
		};
		CARGO_annunFWD = new Offset<byte>(groupName, 26051);
		CARGO_annunAFT = new Offset<byte>(groupName, 26052);
		CARGO_annunDETECTOR_FAULT = new Offset<byte>(groupName, 26053);
		CARGO_annunDISCH = new Offset<byte>(groupName, 26054);
		HGS_annunRWY = new Offset<byte>(groupName, 26055);
		HGS_annunGS = new Offset<byte>(groupName, 26056);
		HGS_annunFAULT = new Offset<byte>(groupName, 26057);
		HGS_annunCLR = new Offset<byte>(groupName, 26058);
		XPDR_XpndrSelector_2 = new Offset<byte>(groupName, 26059);
		XPDR_AltSourceSel_2 = new Offset<byte>(groupName, 26060);
		XPDR_ModeSel = new Offset<byte>(groupName, 26061);
		XPDR_annunFAIL = new Offset<byte>(groupName, 26062);
		LTS_PedFloodKnob = new Offset<byte>(groupName, 26063);
		LTS_PedPanelKnob = new Offset<byte>(groupName, 26064);
		TRIM_StabTrimSw_NORMAL = new Offset<byte>(groupName, 26065);
		PED_annunLOCK_FAIL = new Offset<byte>(groupName, 26066);
		PED_annunAUTO_UNLK = new Offset<byte>(groupName, 26067);
		PED_FltDkDoorSel = new Offset<byte>(groupName, 26068);
		ENG_StartValve = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26069),
			new Offset<byte>(groupName, 26070)
		};
		AIR_DuctPress = new Offset<float>[2]
		{
			new Offset<float>(groupName, 26072),
			new Offset<float>(groupName, 26076)
		};
		COMM_Attend_PressCount = new Offset<byte>(groupName, 26080);
		COMM_GrdCall_PressCount = new Offset<byte>(groupName, 26081);
		COMM_SelectedMic = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 26082),
			new Offset<byte>(groupName, 26083),
			new Offset<byte>(groupName, 26084)
		};
		FUEL_QtyCenter = new Offset<float>(groupName, 26088);
		FUEL_QtyLeft = new Offset<float>(groupName, 26092);
		FUEL_QtyRight = new Offset<float>(groupName, 26096);
		IRS_aligned = new Offset<byte>(groupName, 26100);
		AircraftMode = new Offset<byte>(groupName, 26101);
		WeightInKg = new Offset<byte>(groupName, 26102);
		GPWS_V1CallEnabled = new Offset<byte>(groupName, 26103);
		GroundConnAvailable = new Offset<byte>(groupName, 26104);
		FMC_TakeoffFlaps = new Offset<byte>(groupName, 26105);
		FMC_V1 = new Offset<byte>(groupName, 26106);
		FMC_VR = new Offset<byte>(groupName, 26107);
		FMC_V2 = new Offset<byte>(groupName, 26108);
		FMC_LandingFlaps = new Offset<byte>(groupName, 26109);
		FMC_LandingVREF = new Offset<byte>(groupName, 26110);
		FMC_CruiseAlt = new Offset<ushort>(groupName, 26112);
		FMC_LandingAltitude = new Offset<ushort>(groupName, 26114);
		FMC_TransitionAlt = new Offset<ushort>(groupName, 26116);
		FMC_TransitionLevel = new Offset<ushort>(groupName, 26118);
		FMC_PerfInputComplete = new Offset<byte>(groupName, 26120);
		FMC_DistanceToTOD = new Offset<float>(groupName, 26124);
		FMC_DistanceToDest = new Offset<float>(groupName, 26128);
		FMC_flightNumber = new Offset<string>(groupName, 26132, 9);
		COMM_ReceiverSwitches = new Offset<uint>[3]
		{
			new Offset<uint>(groupName, 27648),
			new Offset<uint>(groupName, 27652),
			new Offset<uint>(groupName, 27656)
		};
		MAIN_annunAP_Amber = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27660),
			new Offset<byte>(groupName, 27661)
		};
		MAIN_annunAT_Amber = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27662),
			new Offset<byte>(groupName, 27663)
		};
		ICE_WindowHeatTestSw = new Offset<int>(groupName, 27664);
		DOOR_annunFWD_ENTRY = new Offset<byte>(groupName, 27668);
		DOOR_annunFWD_SERVICE = new Offset<byte>(groupName, 27669);
		DOOR_annunAIRSTAIR = new Offset<byte>(groupName, 27670);
		DOOR_annunLEFT_FWD_OVERWING = new Offset<byte>(groupName, 27671);
		DOOR_annunRIGHT_FWD_OVERWING = new Offset<byte>(groupName, 27672);
		DOOR_annunFWD_CARGO = new Offset<byte>(groupName, 27673);
		DOOR_annunEQUIP = new Offset<byte>(groupName, 27674);
		DOOR_annunLEFT_AFT_OVERWING = new Offset<byte>(groupName, 27675);
		DOOR_annunRIGHT_AFT_OVERWING = new Offset<byte>(groupName, 27676);
		DOOR_annunAFT_CARGO = new Offset<byte>(groupName, 27677);
		DOOR_annunAFT_ENTRY = new Offset<byte>(groupName, 27678);
		DOOR_annunAFT_SERVICE = new Offset<byte>(groupName, 27679);
		AIR_annunAUTO_FAIL = new Offset<byte>(groupName, 27680);
		AIR_annunOFFSCHED_DESCENT = new Offset<byte>(groupName, 27681);
		AIR_annunALTN = new Offset<byte>(groupName, 27682);
		AIR_annunMANUAL = new Offset<byte>(groupName, 27683);
		AIR_CabinAltNeedle = new Offset<float>(groupName, 27684);
		AIR_CabinDPNeedle = new Offset<float>(groupName, 27688);
		AIR_CabinVSNeedle = new Offset<float>(groupName, 27692);
		AIR_CabinValveNeedle = new Offset<float>(groupName, 27696);
		AIR_TemperatureNeedle = new Offset<float>(groupName, 27700);
		AIR_DuctPressNeedle = new Offset<float>[2]
		{
			new Offset<float>(groupName, 27704),
			new Offset<float>(groupName, 27708)
		};
		ELEC_MeterDisplayTop = new Offset<string>(groupName, 27712, 13);
		ELEC_MeterDisplayBottom = new Offset<string>(groupName, 27725, 13);
		IRS_DisplayLeft = new Offset<string>(groupName, 27738, 7);
		IRS_DisplayRight = new Offset<string>(groupName, 27745, 8);
		IRS_DisplayShowsDots = new Offset<byte>(groupName, 27753);
		ADF_StandbyFrequency = new Offset<uint>(groupName, 27754);
		AFS_AutothrottleServosConnected = new Offset<byte>(groupName, 27758);
		AFS_ControlsPitch = new Offset<byte>(groupName, 27759);
		AFS_ControlsRoll = new Offset<byte>(groupName, 27760);
		ELEC_BusPowered = new Offset<byte>[16]
		{
			new Offset<byte>(groupName, 27761),
			new Offset<byte>(groupName, 27762),
			new Offset<byte>(groupName, 27763),
			new Offset<byte>(groupName, 27764),
			new Offset<byte>(groupName, 27765),
			new Offset<byte>(groupName, 27766),
			new Offset<byte>(groupName, 27767),
			new Offset<byte>(groupName, 27768),
			new Offset<byte>(groupName, 27769),
			new Offset<byte>(groupName, 27770),
			new Offset<byte>(groupName, 27771),
			new Offset<byte>(groupName, 27772),
			new Offset<byte>(groupName, 27773),
			new Offset<byte>(groupName, 27774),
			new Offset<byte>(groupName, 27775),
			new Offset<byte>(groupName, 27776)
		};
		MCP_indication_powered = new Offset<byte>(groupName, 27777);
		AIR_DisplayFltAlt = new Offset<string>(groupName, 27778, 6);
		AIR_DisplayLandAlt = new Offset<string>(groupName, 27784, 6);
		MAIN_annunCABIN_ALTITUDE = new Offset<byte>(groupName, 27790);
		MAIN_annunTAKEOFF_CONFIG = new Offset<byte>(groupName, 27791);
		CVR_annunTEST = new Offset<byte>(groupName, 27792);
		FUEL_AuxFwd = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27793),
			new Offset<byte>(groupName, 27794)
		};
		FUEL_AuxAft = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27795),
			new Offset<byte>(groupName, 27796)
		};
		FUEL_FWDBleed = new Offset<byte>(groupName, 27797);
		FUEL_AFTBleed = new Offset<byte>(groupName, 27798);
		FUEL_GNDXfr = new Offset<byte>(groupName, 27799);
	}

	~PMDG_737_NGX_Offsets()
	{
		//FSUIPCConnection.DeleteGroup(groupName);
	}

	public void RefreshData()
	{
		lock (lockObject)
		{
			if (FSUIPCConnection.IsConnectionOpenForClass(classInstance))
			{
				FSUIPCConnection.Process(classInstance, groupName);
				return;
			}
			if (classInstance == 0)
			{
				throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "The connection to FSUIPC is not open.");
			}
			throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "The connection to class instance " + classInstance.ToString("D2") + " of WideClient.exe is not open.");
		}
	}
}
