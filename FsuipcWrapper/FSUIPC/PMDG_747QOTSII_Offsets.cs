namespace FSUIPC;

public class PMDG_747QOTSII_Offsets
{
	private static int ID;

	private object lockObject = new object();

	private string groupName;

	public Offset<byte>[] ELEC_GenFieldReset { get; private set; }

	public Offset<byte>[] ELEC_APUFieldReset { get; private set; }

	public Offset<byte> ELEC_SplitSystemBreaker { get; private set; }

	public Offset<byte>[] ELEC_annunGen_FIELD_OFF { get; private set; }

	public Offset<byte>[] ELEC_annunAPU_FIELD_OFF { get; private set; }

	public Offset<byte> ELEC_annunSplitSystemBreaker_OPEN { get; private set; }

	public Offset<byte> FUEL_CWTScavengePump_Sw_ON { get; private set; }

	public Offset<byte> FUEL_Reserve23Xfer_Sw_OPEN { get; private set; }

	public Offset<byte>[] ENG_EECPower_Sw_TEST { get; private set; }

	public Offset<byte>[] FCTL_WingHydValve_Sw_SHUT_OFF { get; private set; }

	public Offset<byte>[] FCTL_TailHydValve_Sw_SHUT_OFF { get; private set; }

	public Offset<byte>[] FCTL_annunTailHydVALVE_CLOSED { get; private set; }

	public Offset<byte>[] FCTL_annunWingHydVALVE_CLOSED { get; private set; }

	public Offset<byte> AIR_LowerLobeFlowRate_Selector { get; private set; }

	public Offset<byte> AIR_LowerLobeAftCargoHt_Selector { get; private set; }

	public Offset<byte>[] IRS_Selector { get; private set; }

	public Offset<byte> IRS_annunON_BAT { get; private set; }

	public Offset<byte>[] ELEC_annunUtilOFF { get; private set; }

	public Offset<byte> ELEC_Battery_Sw_ON { get; private set; }

	public Offset<byte> ELEC_APU_Selector { get; private set; }

	public Offset<byte> ELEC_StandbyPowerSw { get; private set; }

	public Offset<byte>[] ELEC_APUGen_Sw_ON { get; private set; }

	public Offset<byte>[] ELEC_UtilSw { get; private set; }

	public Offset<byte>[] ELEC_BusTie_Sw_AUTO { get; private set; }

	public Offset<byte>[] ELEC_annunBusTieISLN { get; private set; }

	public Offset<byte>[] ELEC_Gen_Sw_ON { get; private set; }

	public Offset<byte>[] ELEC_IDGDiscSw { get; private set; }

	public Offset<byte>[] ELEC_ExtPwrSw { get; private set; }

	public Offset<byte>[] ELEC_annunExtPowr_ON { get; private set; }

	public Offset<byte>[] ELEC_annunExtPowr_AVAI { get; private set; }

	public Offset<byte>[] ELEC_annunAPUGen_ON { get; private set; }

	public Offset<byte>[] ELEC_annunAPUGen_AVAIL { get; private set; }

	public Offset<byte>[] ELEC_annunGenOFF { get; private set; }

	public Offset<byte>[] ELEC_annunIDGDiscDRIVE { get; private set; }

	public Offset<byte>[] HYD_EnginePump_Sw_ON { get; private set; }

	public Offset<byte>[] HYD_DemandPump_Selector { get; private set; }

	public Offset<byte>[] HYD_annunSYS_FAULT { get; private set; }

	public Offset<byte>[] HYD_annunEnginePumpPRESS { get; private set; }

	public Offset<byte>[] HYD_annunDemandPumpPRESS { get; private set; }

	public Offset<byte> HYD_RamAirTurbineSw { get; private set; }

	public Offset<byte> HYD_annunRamAirTurbinePRESS { get; private set; }

	public Offset<byte> HYD_annunRamAirTurbineUNLKD { get; private set; }

	public Offset<byte>[] FIRE_EngineHandle { get; private set; }

	public Offset<byte>[] FIRE_EngineHandleUnlock_Sw { get; private set; }

	public Offset<byte>[] FIRE_annunENG_BTL_DISCH { get; private set; }

	public Offset<byte>[] FIRE_CargoFire_Sw_Arm { get; private set; }

	public Offset<byte>[] FIRE_annunCargoFire { get; private set; }

	public Offset<byte> FIRE_MainDeckFire_Sw_Arm { get; private set; }

	public Offset<byte> FIRE_annunMainDeckFire { get; private set; }

	public Offset<byte> FIRE_CargoFireDisch_Sw { get; private set; }

	public Offset<byte> FIRE_annunCargoDISCH { get; private set; }

	public Offset<byte> FIRE_FireOvhtTest_Sw { get; private set; }

	public Offset<byte> FIRE_APUHandle { get; private set; }

	public Offset<byte> FIRE_APUHandleUnlock_Sw { get; private set; }

	public Offset<byte> FIRE_annunAPU_BTL_DISCH { get; private set; }

	public Offset<byte>[] ENG_EECMode_Sw_NORM { get; private set; }

	public Offset<byte>[] ENG_Start_Sw_Pulled { get; private set; }

	public Offset<byte> ENG_ConIginition_Sw_ON { get; private set; }

	public Offset<byte> ENG_StbyIginition_Selector { get; private set; }

	public Offset<uint> ENG_AutoIginition_Selector { get; private set; }

	public Offset<byte> ENG_Autostart_Sw_ON { get; private set; }

	public Offset<byte>[] ENG_Start_Light { get; private set; }

	public Offset<byte>[] ENG_annunALTN { get; private set; }

	public Offset<byte>[] FUEL_CrossFeed_Sw { get; private set; }

	public Offset<byte>[] FUEL_MainPumpFwd_Sw { get; private set; }

	public Offset<byte>[] FUEL_MainPumpAft_Sw { get; private set; }

	public Offset<byte>[] FUEL_OvrdPumpFwd_Sw { get; private set; }

	public Offset<byte>[] FUEL_OvrdPumpAft_Sw { get; private set; }

	public Offset<byte>[] FUEL_PumpStab_Sw { get; private set; }

	public Offset<byte>[] FUEL_PumpCtr_Sw { get; private set; }

	public Offset<byte> FUEL_XferMain14_Sw { get; private set; }

	public Offset<byte>[] FUEL_JettisonNozle_Sw { get; private set; }

	public Offset<byte> FUEL_JettisonArm_Selector { get; private set; }

	public Offset<byte> FUEL_FuelToRemain_Selector { get; private set; }

	public Offset<byte>[] FUEL_annunXFEED_VALVE { get; private set; }

	public Offset<byte>[] FUEL_annunPRESS_MainFwd { get; private set; }

	public Offset<byte>[] FUEL_annunPRESS_MainAft { get; private set; }

	public Offset<byte>[] FUEL_annunPRESS_OvrdFwd { get; private set; }

	public Offset<byte>[] FUEL_annunPRESS_OvrdAft { get; private set; }

	public Offset<byte>[] FUEL_annunPRESS_Stab { get; private set; }

	public Offset<byte>[] FUEL_annunPRESS_Ctr { get; private set; }

	public Offset<byte>[] FUEL_annunJettisonNozleVALVE { get; private set; }

	public Offset<byte> ICE_WingAntiIceSw { get; private set; }

	public Offset<byte>[] ICE_EngAntiIceSw { get; private set; }

	public Offset<byte>[] ICE_annunEngAntiIceVALVE { get; private set; }

	public Offset<byte> ICE_annunWingAntiIceVALVE { get; private set; }

	public Offset<byte>[] ICE_WindowHeat_Sw_ON { get; private set; }

	public Offset<byte>[] ICE_annunWindowHeatINOP { get; private set; }

	public Offset<byte>[] APU_annunLOW_OIL_PRESSURE { get; private set; }

	public Offset<byte>[] APU_annunFAULT { get; private set; }

	public Offset<byte>[] APU_annunOVERSPEED { get; private set; }

	public Offset<byte> LTS_DomeLightKnob { get; private set; }

	public Offset<byte> LTS_CktBkrOverheadKnob { get; private set; }

	public Offset<byte> LTS_GlareshieldPNLlKnob { get; private set; }

	public Offset<byte> LTS_GlareshieldFLOODKnob { get; private set; }

	public Offset<byte> LTS_AisleStandPNLKnob { get; private set; }

	public Offset<byte> LTS_AisleStandFLOODKnob { get; private set; }

	public Offset<byte> LTS_Storm_Sw_ON { get; private set; }

	public Offset<byte> LTS_IndLightsTestSw { get; private set; }

	public Offset<byte>[] LTS_LandingLights_Sw_ON { get; private set; }

	public Offset<byte> LTS_Beacon_Sw { get; private set; }

	public Offset<byte> LTS_NAV_Sw_ON { get; private set; }

	public Offset<byte> LTS_Logo_Sw_ON { get; private set; }

	public Offset<byte> LTS_Wing_Sw_ON { get; private set; }

	public Offset<byte>[] LTS_RunwayTurnoff_Sw_ON { get; private set; }

	public Offset<byte> LTS_Taxi_Sw_ON { get; private set; }

	public Offset<byte> LTS_Strobe_Sw_ON { get; private set; }

	public Offset<byte> AIR_LdgAlt_PushOn_Sw { get; private set; }

	public Offset<byte> AIR_LdgAlt_Selector { get; private set; }

	public Offset<byte>[] AIR_OutflowValveMan_Sw { get; private set; }

	public Offset<uint>[] AIR_OutflowValveNeedle { get; private set; }

	public Offset<byte> AIR_OutflowValves_Selector { get; private set; }

	public Offset<byte> AIR_CabinAltAuto_Selector { get; private set; }

	public Offset<byte> AIR_SmokeEvacHandle_Pulled { get; private set; }

	public Offset<byte>[] AIR_Pack_Selector { get; private set; }

	public Offset<byte>[] AIR_TrimAir_Sw_On { get; private set; }

	public Offset<byte>[] AIR_RecircFan_Sw_On { get; private set; }

	public Offset<byte>[] AIR_TempSelector { get; private set; }

	public Offset<byte> AIR_PackReset_Sw_Pushed { get; private set; }

	public Offset<byte> AIR_EquipCooling_Selector { get; private set; }

	public Offset<byte> AIR_HighFlow_Sw_On { get; private set; }

	public Offset<byte> AIR_Gasper_Sw_On { get; private set; }

	public Offset<byte> AIR_FltDeckFan_Sw_On { get; private set; }

	public Offset<byte> AIR_AftCargoHeat_Sw_On { get; private set; }

	public Offset<byte> AIR_ZoneReset_Sw_Pushed { get; private set; }

	public Offset<byte> AIR_AltnVent_Sw_On { get; private set; }

	public Offset<byte> AIR_AltnVent_Selector { get; private set; }

	public Offset<byte>[] AIR_annunPackOFF { get; private set; }

	public Offset<byte> AIR_annunPack_SYS_FAIL { get; private set; }

	public Offset<byte> AIR_annunZone_SYS_FAIL { get; private set; }

	public Offset<byte> AIR_annunAftCragoHeat_TEMP { get; private set; }

	public Offset<byte>[] AIR_EngBleedAir_Sw_ON { get; private set; }

	public Offset<byte> AIR_APUBleedAir_Sw_ON { get; private set; }

	public Offset<byte>[] AIR_IsolationValve_Sw { get; private set; }

	public Offset<byte>[] AIR_annunEngBleedAirOFF { get; private set; }

	public Offset<byte> AIR_annunAPUBleedAirVALVE { get; private set; }

	public Offset<byte>[] AIR_annunIsolationVALVE { get; private set; }

	public Offset<byte>[] AIR_annun_SYS_FAULT { get; private set; }

	public Offset<byte> LTS_EmerLightsSelector { get; private set; }

	public Offset<byte> COMM_CAPTAudio_Selector { get; private set; }

	public Offset<byte> COMM_OBSAudio_Selector { get; private set; }

	public Offset<byte> COMM_ServiceInterphoneSw { get; private set; }

	public Offset<byte> COMM_CargoCabinInterphoneSw { get; private set; }

	public Offset<byte> OXY_Oxygen_Sw_On { get; private set; }

	public Offset<byte>[] FCTL_YawDamper_Sw { get; private set; }

	public Offset<byte>[] FCTL_annunYawDamperINOP { get; private set; }

	public Offset<byte>[] WARN_Reset_Sw_Pushed { get; private set; }

	public Offset<byte>[] WARN_annunMASTER_WARNING { get; private set; }

	public Offset<byte>[] WARN_annunMASTER_CAUTION { get; private set; }

	public Offset<byte>[] EFIS_MinsSelBARO { get; private set; }

	public Offset<byte>[] EFIS_BaroSelHPA { get; private set; }

	public Offset<byte>[] EFIS_VORADFSel1 { get; private set; }

	public Offset<byte>[] EFIS_VORADFSel2 { get; private set; }

	public Offset<byte>[] EFIS_ModeSel { get; private set; }

	public Offset<byte>[] EFIS_RangeSel { get; private set; }

	public Offset<byte>[] EFIS_MinsKnob { get; private set; }

	public Offset<byte>[] EFIS_BaroKnob { get; private set; }

	public Offset<byte>[] EFIS_MinsRST_Sw_Pushed { get; private set; }

	public Offset<byte>[] EFIS_BaroSTD_Sw_Pushed { get; private set; }

	public Offset<byte>[] EFIS_ModeCTR_Sw_Pushed { get; private set; }

	public Offset<byte>[] EFIS_RangeTFC_Sw_Pushed { get; private set; }

	public Offset<byte>[] EFIS_WXR_Sw_Pushed { get; private set; }

	public Offset<byte>[] EFIS_STA_Sw_Pushed { get; private set; }

	public Offset<byte>[] EFIS_WPT_Sw_Pushed { get; private set; }

	public Offset<byte>[] EFIS_ARPT_Sw_Pushed { get; private set; }

	public Offset<byte>[] EFIS_DATA_Sw_Pushed { get; private set; }

	public Offset<byte>[] EFIS_POS_Sw_Pushed { get; private set; }

	public Offset<byte>[] EFIS_TERR_Sw_Pushed { get; private set; }

	public Offset<byte> DSP_L_INBD_Sw { get; private set; }

	public Offset<byte> DSP_R_INBD_Sw { get; private set; }

	public Offset<byte> DSP_LWR_CTR_Sw { get; private set; }

	public Offset<byte> DSP_ENG_Sw { get; private set; }

	public Offset<byte> DSP_STAT_Sw { get; private set; }

	public Offset<byte> DSP_ELEC_Sw { get; private set; }

	public Offset<byte> DSP_HYD_Sw { get; private set; }

	public Offset<byte> DSP_FUEL_Sw { get; private set; }

	public Offset<byte> DSP_ECS_Sw { get; private set; }

	public Offset<byte> DSP_DRS_Sw { get; private set; }

	public Offset<byte> DSP_GEAR_Sw { get; private set; }

	public Offset<byte> DSP_FCTL_Sw { get; private set; }

	public Offset<byte> DSP_INFO_Sw { get; private set; }

	public Offset<byte> DSP_CHKL_Sw { get; private set; }

	public Offset<byte> DSP_NAV_Sw { get; private set; }

	public Offset<byte> DSP_CANC_Sw { get; private set; }

	public Offset<byte> DSP_RCL_Sw { get; private set; }

	public Offset<byte> DSP_annunL_INBD { get; private set; }

	public Offset<byte> DSP_annunR_INBD { get; private set; }

	public Offset<byte> DSP_annunLWR_CTR { get; private set; }

	public Offset<float> MCP_IASMach { get; private set; }

	public Offset<byte> MCP_IASBlank { get; private set; }

	public Offset<ushort> MCP_Heading { get; private set; }

	public Offset<ushort> MCP_Altitude { get; private set; }

	public Offset<short> MCP_VertSpeed { get; private set; }

	public Offset<byte> MCP_VertSpeedBlank { get; private set; }

	public Offset<byte>[] MCP_FD_Sw_On { get; private set; }

	public Offset<byte> MCP_ATArm_Sw_On { get; private set; }

	public Offset<byte> MCP_BankLimitSel { get; private set; }

	public Offset<byte> MCP_DisengageBar { get; private set; }

	public Offset<byte> MCP_Speed_Dial { get; private set; }

	public Offset<byte> MCP_Heading_Dial { get; private set; }

	public Offset<byte> MCP_Altitude_Dial { get; private set; }

	public Offset<byte> MCP_VS_Wheel { get; private set; }

	public Offset<byte>[] MCP_AP_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_THR_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_SPD_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_LNAV_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_VNAV_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_FLCH_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_HDG_HOLD_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_VS_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_ALT_HOLD_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_LOC_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_APP_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_Speeed_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_Heading_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_Altitude_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_IAS_MACH_Toggle_Sw_Pushed { get; private set; }

	public Offset<byte>[] MCP_annunAP { get; private set; }

	public Offset<byte> MCP_annunTHR { get; private set; }

	public Offset<byte> MCP_annunSPD { get; private set; }

	public Offset<byte> MCP_annunLNAV { get; private set; }

	public Offset<byte> MCP_annunVNAV { get; private set; }

	public Offset<byte> MCP_annunFLCH { get; private set; }

	public Offset<byte> MCP_annunHDG_HOLD { get; private set; }

	public Offset<byte> MCP_annunVS { get; private set; }

	public Offset<byte> MCP_annunALT_HOLD { get; private set; }

	public Offset<byte> MCP_annunLOC { get; private set; }

	public Offset<byte> MCP_annunAPP { get; private set; }

	public Offset<byte> DSP_InbdDspl_L_Selector { get; private set; }

	public Offset<byte> DSP_LwrDspl_L_Selector { get; private set; }

	public Offset<byte> DSP_InbdDspl_R_Selector { get; private set; }

	public Offset<byte> DSP_LwrDspl_R_Selector { get; private set; }

	public Offset<byte> ISP_FMC_Selector { get; private set; }

	public Offset<byte> ISP_EIU_C_Selector { get; private set; }

	public Offset<byte> LTS_UpperDsplBRIGHTNESSKnob { get; private set; }

	public Offset<byte> LTS_LowerDsplBRIGHTNESSKnob { get; private set; }

	public Offset<byte> EICAS_EventRcd_Sw_Pushed { get; private set; }

	public Offset<byte> EFIS_HdgRef_Sw_Norm { get; private set; }

	public Offset<byte> FCTL_AltnFlaps_Sw_ARM { get; private set; }

	public Offset<byte> FCTL_AltnFlaps_Control_Sw { get; private set; }

	public Offset<byte> GEAR_Lever { get; private set; }

	public Offset<byte> GEAR_LockOvrd_Sw { get; private set; }

	public Offset<byte> GEAR_AltnGearNoseBody_Sw_DPushed { get; private set; }

	public Offset<byte> GEAR_AltnGearWing_Sw_DPushed { get; private set; }

	public Offset<byte> GPWS_GSInhibit_Sw { get; private set; }

	public Offset<byte> GPWS_annunGND_PROX_top { get; private set; }

	public Offset<byte> GPWS_annunGND_PROX_bottom { get; private set; }

	public Offset<byte> GPWS_FlapInhibitSw_OVRD { get; private set; }

	public Offset<byte> GPWS_GearInhibitSw_OVRD { get; private set; }

	public Offset<byte> GPWS_TerrInhibitSw_OVRD { get; private set; }

	public Offset<byte> ISFD_Baro_Sw_Pushed { get; private set; }

	public Offset<byte> ISFD_RST_Sw_Pushed { get; private set; }

	public Offset<byte> ISFD_Minus_Sw_Pushed { get; private set; }

	public Offset<byte> ISFD_Plus_Sw_Pushed { get; private set; }

	public Offset<byte> ISFD_APP_Sw_Pushed { get; private set; }

	public Offset<byte> ISFD_HP_IN_Sw_Pushed { get; private set; }

	public Offset<byte> ISP_FltDir_L_Selector { get; private set; }

	public Offset<byte> ISP_Nav_L_Selector { get; private set; }

	public Offset<byte> ISP_EIU_L_Selector { get; private set; }

	public Offset<byte> ISP_IRS_L_Selector { get; private set; }

	public Offset<byte> ISP_AirData_L_Selector { get; private set; }

	public Offset<int> BRAKES_BrakePressNeedle { get; private set; }

	public Offset<byte> BRAKES_annunBRAKE_SOURCE { get; private set; }

	public Offset<byte> ISP_FltDir_R_Selector { get; private set; }

	public Offset<byte> ISP_Nav_R_Selector { get; private set; }

	public Offset<byte> ISP_EIU_R_Selector { get; private set; }

	public Offset<byte> ISP_IRS_R_Selector { get; private set; }

	public Offset<byte> ISP_AirData_R_Selector { get; private set; }

	public Offset<byte> LTS_LeftFwdPanelPNLKnob { get; private set; }

	public Offset<byte> LTS_LeftFwdPanelFLOODKnob { get; private set; }

	public Offset<byte> LTS_LeftOutbdDsplBRIGHTNESSKnob { get; private set; }

	public Offset<byte> LTS_LeftInbdDsplBRIGHTNESSKnob { get; private set; }

	public Offset<byte> LTS_RightFwdPanelPNLKnob { get; private set; }

	public Offset<byte> LTS_RightFwdPanelFLOODKnob { get; private set; }

	public Offset<byte> LTS_RightInbdDsplBRIGHTNESSKnob { get; private set; }

	public Offset<byte> LTS_RightOutbdDsplBRIGHTNESSKnob { get; private set; }

	public Offset<byte>[] AIR_ShoulderHeaterKnob { get; private set; }

	public Offset<byte>[] AIR_FootHeaterSelector { get; private set; }

	public Offset<byte>[] AIR_WShldAirSelector { get; private set; }

	public Offset<byte>[] CHR_Chr_Sw_Pushed { get; private set; }

	public Offset<byte>[] CHR_Date_Sw_Pushed { get; private set; }

	public Offset<byte>[] CHR_Set_Selector { get; private set; }

	public Offset<byte>[] CHR_ET_Selector { get; private set; }

	public Offset<byte> FCTL_StabCutOutSw_2_NORMAL { get; private set; }

	public Offset<byte> FCTL_StabCutOutSw_3_NORMAL { get; private set; }

	public Offset<byte> FCTL_AltnPitch_Switches { get; private set; }

	public Offset<byte> FCTL_Speedbrake_Lever { get; private set; }

	public Offset<byte> FCTL_Flaps_Lever { get; private set; }

	public Offset<byte>[] ENG_FuelControl_Sw_RUN { get; private set; }

	public Offset<byte> BRAKES_ParkingBrakeLeverOn { get; private set; }

	public Offset<byte>[] CDU_annunEXEC { get; private set; }

	public Offset<byte>[] CDU_annunDSPY { get; private set; }

	public Offset<byte>[] CDU_annunFAIL { get; private set; }

	public Offset<byte>[] CDU_annunMSG { get; private set; }

	public Offset<byte>[] CDU_annunOFST { get; private set; }

	public Offset<byte>[] CDU_BrtKnob { get; private set; }

	public Offset<byte>[] COMM_SelectedMic { get; private set; }

	public Offset<byte>[] COMM_ReceiverSwitches { get; private set; }

	public Offset<byte>[] COMM_SelectedRadio { get; private set; }

	public Offset<byte>[] COMM_RadioTransfer_Sw_Pushed { get; private set; }

	public Offset<byte>[] COMM_RadioPanelOff { get; private set; }

	public Offset<byte>[] COMM_annunAM { get; private set; }

	public Offset<byte> XPDR_XpndrSelector_R { get; private set; }

	public Offset<byte> XPDR_ModeSel { get; private set; }

	public Offset<byte> XPDR_Ident_Sw_Pushed { get; private set; }

	public Offset<byte> BRAKES_AutobrakeSelector { get; private set; }

	public Offset<byte> FCTL_AileronTrim_Switches { get; private set; }

	public Offset<byte> FCTL_RudderTrim_Knob { get; private set; }

	public Offset<byte> SIGNS_NoSmokingSelector { get; private set; }

	public Offset<byte> SIGNS_SeatBeltsSelector { get; private set; }

	public Offset<byte> EVAC_Command_Sw_ON { get; private set; }

	public Offset<byte> EVAC_PressToTest_Sw_Pressed { get; private set; }

	public Offset<byte> EVAC_HornSutOff_Sw_Pulled { get; private set; }

	public Offset<byte> EVAC_LightIlluminated { get; private set; }

	public Offset<byte>[] DOOR_state { get; private set; }

	public Offset<byte>[] ENG_StartValve { get; private set; }

	public Offset<float>[] AIR_DuctPress { get; private set; }

	public Offset<float>[] FUEL_TankQty { get; private set; }

	public Offset<byte> IRS_aligned { get; private set; }

	public Offset<byte> AircraftModel { get; private set; }

	public Offset<byte> WeightInKg { get; private set; }

	public Offset<byte> GPWS_V1CallEnabled { get; private set; }

	public Offset<byte> GroundConnAvailable { get; private set; }

	public Offset<byte> FMC_TakeoffFlaps { get; private set; }

	public Offset<byte> FMC_V1 { get; private set; }

	public Offset<byte> FMC_VR { get; private set; }

	public Offset<byte> FMC_V2 { get; private set; }

	public Offset<byte> FMC_LandingFlaps { get; private set; }

	public Offset<byte> FMC_LandingVREF { get; private set; }

	public Offset<ushort> FMC_CruiseAlt { get; private set; }

	public Offset<short> FMC_LandingAltitude { get; private set; }

	public Offset<ushort> FMC_TransitionAlt { get; private set; }

	public Offset<ushort> FMC_TransitionLevel { get; private set; }

	public Offset<byte> FMC_PerfInputComplete { get; private set; }

	public Offset<float> FMC_DistanceToTOD { get; private set; }

	public Offset<float> FMC_DistanceToDest { get; private set; }

	public Offset<string> FMC_flightNumber { get; private set; }

	public Offset<byte> ELEC_annunBatteryOFF { get; private set; }

	public Offset<byte> FIRE_annunCargoDEPRESS { get; private set; }

	public Offset<byte> MCP_panelPowered { get; private set; }

	public Offset<byte>[] COMM_RadioPanelPowered { get; private set; }

	public Offset<byte>[] COMM_AudioControlPanelPowered { get; private set; }

	public Offset<byte> TCAS_ATC_panelPowered { get; private set; }

	public Offset<byte>[] FIRE_HandleIllumination { get; private set; }

	public Offset<byte> WheelChocksSet { get; private set; }

	public PMDG_747QOTSII_Offsets()
	{
		ID++;
		groupName = "~~~ PMDG 747 OFFSETS " + ID + " ~~~";
		ELEC_GenFieldReset = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25632),
			new Offset<byte>(groupName, 25633),
			new Offset<byte>(groupName, 25634),
			new Offset<byte>(groupName, 25635)
		};
		ELEC_APUFieldReset = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25636),
			new Offset<byte>(groupName, 25637)
		};
		ELEC_SplitSystemBreaker = new Offset<byte>(groupName, 25638);
		ELEC_annunGen_FIELD_OFF = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25639),
			new Offset<byte>(groupName, 25640),
			new Offset<byte>(groupName, 25641),
			new Offset<byte>(groupName, 25642)
		};
		ELEC_annunAPU_FIELD_OFF = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25643),
			new Offset<byte>(groupName, 25644)
		};
		ELEC_annunSplitSystemBreaker_OPEN = new Offset<byte>(groupName, 25645);
		FUEL_CWTScavengePump_Sw_ON = new Offset<byte>(groupName, 25646);
		FUEL_Reserve23Xfer_Sw_OPEN = new Offset<byte>(groupName, 25647);
		ENG_EECPower_Sw_TEST = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25648),
			new Offset<byte>(groupName, 25649),
			new Offset<byte>(groupName, 25650),
			new Offset<byte>(groupName, 25651)
		};
		FCTL_WingHydValve_Sw_SHUT_OFF = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25652),
			new Offset<byte>(groupName, 25653),
			new Offset<byte>(groupName, 25654),
			new Offset<byte>(groupName, 25655)
		};
		FCTL_TailHydValve_Sw_SHUT_OFF = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25656),
			new Offset<byte>(groupName, 25657),
			new Offset<byte>(groupName, 25658),
			new Offset<byte>(groupName, 25659)
		};
		FCTL_annunTailHydVALVE_CLOSED = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25660),
			new Offset<byte>(groupName, 25661),
			new Offset<byte>(groupName, 25662),
			new Offset<byte>(groupName, 25663)
		};
		FCTL_annunWingHydVALVE_CLOSED = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25664),
			new Offset<byte>(groupName, 25665),
			new Offset<byte>(groupName, 25666),
			new Offset<byte>(groupName, 25667)
		};
		AIR_LowerLobeFlowRate_Selector = new Offset<byte>(groupName, 25668);
		AIR_LowerLobeAftCargoHt_Selector = new Offset<byte>(groupName, 25669);
		IRS_Selector = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 25670),
			new Offset<byte>(groupName, 25671),
			new Offset<byte>(groupName, 25672)
		};
		IRS_annunON_BAT = new Offset<byte>(groupName, 25673);
		ELEC_annunUtilOFF = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25674),
			new Offset<byte>(groupName, 25675)
		};
		ELEC_Battery_Sw_ON = new Offset<byte>(groupName, 25676);
		ELEC_APU_Selector = new Offset<byte>(groupName, 25677);
		ELEC_StandbyPowerSw = new Offset<byte>(groupName, 25678);
		ELEC_APUGen_Sw_ON = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25679),
			new Offset<byte>(groupName, 25680)
		};
		ELEC_UtilSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25681),
			new Offset<byte>(groupName, 25682)
		};
		ELEC_BusTie_Sw_AUTO = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25683),
			new Offset<byte>(groupName, 25684),
			new Offset<byte>(groupName, 25685),
			new Offset<byte>(groupName, 25686)
		};
		ELEC_annunBusTieISLN = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25687),
			new Offset<byte>(groupName, 25688),
			new Offset<byte>(groupName, 25689),
			new Offset<byte>(groupName, 25690)
		};
		ELEC_Gen_Sw_ON = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25691),
			new Offset<byte>(groupName, 25692),
			new Offset<byte>(groupName, 25693),
			new Offset<byte>(groupName, 25694)
		};
		ELEC_IDGDiscSw = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25695),
			new Offset<byte>(groupName, 25696),
			new Offset<byte>(groupName, 25697),
			new Offset<byte>(groupName, 25698)
		};
		ELEC_ExtPwrSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25699),
			new Offset<byte>(groupName, 25700)
		};
		ELEC_annunExtPowr_ON = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25701),
			new Offset<byte>(groupName, 25702)
		};
		ELEC_annunExtPowr_AVAI = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25703),
			new Offset<byte>(groupName, 25704)
		};
		ELEC_annunAPUGen_ON = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25705),
			new Offset<byte>(groupName, 25706)
		};
		ELEC_annunAPUGen_AVAIL = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25707),
			new Offset<byte>(groupName, 25708)
		};
		ELEC_annunGenOFF = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25709),
			new Offset<byte>(groupName, 25710),
			new Offset<byte>(groupName, 25711),
			new Offset<byte>(groupName, 25712)
		};
		ELEC_annunIDGDiscDRIVE = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25713),
			new Offset<byte>(groupName, 25714),
			new Offset<byte>(groupName, 25715),
			new Offset<byte>(groupName, 25716)
		};
		HYD_EnginePump_Sw_ON = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25717),
			new Offset<byte>(groupName, 25718),
			new Offset<byte>(groupName, 25719),
			new Offset<byte>(groupName, 25720)
		};
		HYD_DemandPump_Selector = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25721),
			new Offset<byte>(groupName, 25722),
			new Offset<byte>(groupName, 25723),
			new Offset<byte>(groupName, 25724)
		};
		HYD_annunSYS_FAULT = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25725),
			new Offset<byte>(groupName, 25726),
			new Offset<byte>(groupName, 25727),
			new Offset<byte>(groupName, 25728)
		};
		HYD_annunEnginePumpPRESS = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25729),
			new Offset<byte>(groupName, 25730),
			new Offset<byte>(groupName, 25731),
			new Offset<byte>(groupName, 25732)
		};
		HYD_annunDemandPumpPRESS = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25733),
			new Offset<byte>(groupName, 25734),
			new Offset<byte>(groupName, 25735),
			new Offset<byte>(groupName, 25736)
		};
		HYD_RamAirTurbineSw = new Offset<byte>(groupName, 25737);
		HYD_annunRamAirTurbinePRESS = new Offset<byte>(groupName, 25738);
		HYD_annunRamAirTurbineUNLKD = new Offset<byte>(groupName, 25739);
		FIRE_EngineHandle = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25740),
			new Offset<byte>(groupName, 25741),
			new Offset<byte>(groupName, 25742),
			new Offset<byte>(groupName, 25743)
		};
		FIRE_EngineHandleUnlock_Sw = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25744),
			new Offset<byte>(groupName, 25745),
			new Offset<byte>(groupName, 25746),
			new Offset<byte>(groupName, 25747)
		};
		FIRE_annunENG_BTL_DISCH = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25748),
			new Offset<byte>(groupName, 25749),
			new Offset<byte>(groupName, 25750),
			new Offset<byte>(groupName, 25751)
		};
		FIRE_CargoFire_Sw_Arm = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25752),
			new Offset<byte>(groupName, 25753)
		};
		FIRE_annunCargoFire = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25754),
			new Offset<byte>(groupName, 25755)
		};
		FIRE_MainDeckFire_Sw_Arm = new Offset<byte>(groupName, 25756);
		FIRE_annunMainDeckFire = new Offset<byte>(groupName, 25757);
		FIRE_CargoFireDisch_Sw = new Offset<byte>(groupName, 25758);
		FIRE_annunCargoDISCH = new Offset<byte>(groupName, 25759);
		FIRE_FireOvhtTest_Sw = new Offset<byte>(groupName, 25760);
		FIRE_APUHandle = new Offset<byte>(groupName, 25761);
		FIRE_APUHandleUnlock_Sw = new Offset<byte>(groupName, 25762);
		FIRE_annunAPU_BTL_DISCH = new Offset<byte>(groupName, 25763);
		ENG_EECMode_Sw_NORM = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25764),
			new Offset<byte>(groupName, 25765),
			new Offset<byte>(groupName, 25766),
			new Offset<byte>(groupName, 25767)
		};
		ENG_Start_Sw_Pulled = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25768),
			new Offset<byte>(groupName, 25769),
			new Offset<byte>(groupName, 25770),
			new Offset<byte>(groupName, 25771)
		};
		ENG_ConIginition_Sw_ON = new Offset<byte>(groupName, 25772);
		ENG_StbyIginition_Selector = new Offset<byte>(groupName, 25773);
		ENG_AutoIginition_Selector = new Offset<uint>(groupName, 25774);
		ENG_Autostart_Sw_ON = new Offset<byte>(groupName, 25778);
		ENG_Start_Light = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25779),
			new Offset<byte>(groupName, 25780),
			new Offset<byte>(groupName, 25781),
			new Offset<byte>(groupName, 25782)
		};
		ENG_annunALTN = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25783),
			new Offset<byte>(groupName, 25784),
			new Offset<byte>(groupName, 25785),
			new Offset<byte>(groupName, 25786)
		};
		FUEL_CrossFeed_Sw = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25787),
			new Offset<byte>(groupName, 25788),
			new Offset<byte>(groupName, 25789),
			new Offset<byte>(groupName, 25790)
		};
		FUEL_MainPumpFwd_Sw = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25791),
			new Offset<byte>(groupName, 25792),
			new Offset<byte>(groupName, 25793),
			new Offset<byte>(groupName, 25794)
		};
		FUEL_MainPumpAft_Sw = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25795),
			new Offset<byte>(groupName, 25796),
			new Offset<byte>(groupName, 25797),
			new Offset<byte>(groupName, 25798)
		};
		FUEL_OvrdPumpFwd_Sw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25799),
			new Offset<byte>(groupName, 25800)
		};
		FUEL_OvrdPumpAft_Sw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25801),
			new Offset<byte>(groupName, 25802)
		};
		FUEL_PumpStab_Sw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25803),
			new Offset<byte>(groupName, 25804)
		};
		FUEL_PumpCtr_Sw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25805),
			new Offset<byte>(groupName, 25806)
		};
		FUEL_XferMain14_Sw = new Offset<byte>(groupName, 25807);
		FUEL_JettisonNozle_Sw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25808),
			new Offset<byte>(groupName, 25809)
		};
		FUEL_JettisonArm_Selector = new Offset<byte>(groupName, 25810);
		FUEL_FuelToRemain_Selector = new Offset<byte>(groupName, 25811);
		FUEL_annunXFEED_VALVE = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25812),
			new Offset<byte>(groupName, 25813),
			new Offset<byte>(groupName, 25814),
			new Offset<byte>(groupName, 25815)
		};
		FUEL_annunPRESS_MainFwd = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25816),
			new Offset<byte>(groupName, 25817),
			new Offset<byte>(groupName, 25818),
			new Offset<byte>(groupName, 25819)
		};
		FUEL_annunPRESS_MainAft = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25820),
			new Offset<byte>(groupName, 25821),
			new Offset<byte>(groupName, 25822),
			new Offset<byte>(groupName, 25823)
		};
		FUEL_annunPRESS_OvrdFwd = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25824),
			new Offset<byte>(groupName, 25825)
		};
		FUEL_annunPRESS_OvrdAft = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25826),
			new Offset<byte>(groupName, 25827)
		};
		FUEL_annunPRESS_Stab = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25828),
			new Offset<byte>(groupName, 25829)
		};
		FUEL_annunPRESS_Ctr = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25830),
			new Offset<byte>(groupName, 25831)
		};
		FUEL_annunJettisonNozleVALVE = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25832),
			new Offset<byte>(groupName, 25833)
		};
		ICE_WingAntiIceSw = new Offset<byte>(groupName, 25834);
		ICE_EngAntiIceSw = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25835),
			new Offset<byte>(groupName, 25836),
			new Offset<byte>(groupName, 25837),
			new Offset<byte>(groupName, 25838)
		};
		ICE_annunEngAntiIceVALVE = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25839),
			new Offset<byte>(groupName, 25840),
			new Offset<byte>(groupName, 25841),
			new Offset<byte>(groupName, 25842)
		};
		ICE_annunWingAntiIceVALVE = new Offset<byte>(groupName, 25843);
		ICE_WindowHeat_Sw_ON = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25844),
			new Offset<byte>(groupName, 25845)
		};
		ICE_annunWindowHeatINOP = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25846),
			new Offset<byte>(groupName, 25847)
		};
		APU_annunLOW_OIL_PRESSURE = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25848),
			new Offset<byte>(groupName, 25849)
		};
		APU_annunFAULT = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25850),
			new Offset<byte>(groupName, 25851)
		};
		APU_annunOVERSPEED = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25852),
			new Offset<byte>(groupName, 25853)
		};
		LTS_DomeLightKnob = new Offset<byte>(groupName, 25854);
		LTS_CktBkrOverheadKnob = new Offset<byte>(groupName, 25855);
		LTS_GlareshieldPNLlKnob = new Offset<byte>(groupName, 25856);
		LTS_GlareshieldFLOODKnob = new Offset<byte>(groupName, 25857);
		LTS_AisleStandPNLKnob = new Offset<byte>(groupName, 25858);
		LTS_AisleStandFLOODKnob = new Offset<byte>(groupName, 25859);
		LTS_Storm_Sw_ON = new Offset<byte>(groupName, 25860);
		LTS_IndLightsTestSw = new Offset<byte>(groupName, 25861);
		LTS_LandingLights_Sw_ON = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25862),
			new Offset<byte>(groupName, 25863),
			new Offset<byte>(groupName, 25864),
			new Offset<byte>(groupName, 25865)
		};
		LTS_Beacon_Sw = new Offset<byte>(groupName, 25866);
		LTS_NAV_Sw_ON = new Offset<byte>(groupName, 25867);
		LTS_Logo_Sw_ON = new Offset<byte>(groupName, 25868);
		LTS_Wing_Sw_ON = new Offset<byte>(groupName, 25869);
		LTS_RunwayTurnoff_Sw_ON = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25870),
			new Offset<byte>(groupName, 25871)
		};
		LTS_Taxi_Sw_ON = new Offset<byte>(groupName, 25872);
		LTS_Strobe_Sw_ON = new Offset<byte>(groupName, 25873);
		AIR_LdgAlt_PushOn_Sw = new Offset<byte>(groupName, 25874);
		AIR_LdgAlt_Selector = new Offset<byte>(groupName, 25875);
		AIR_OutflowValveMan_Sw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25876),
			new Offset<byte>(groupName, 25877)
		};
		AIR_OutflowValveNeedle = new Offset<uint>[2]
		{
			new Offset<uint>(groupName, 25878),
			new Offset<uint>(groupName, 25882)
		};
		AIR_OutflowValves_Selector = new Offset<byte>(groupName, 25886);
		AIR_CabinAltAuto_Selector = new Offset<byte>(groupName, 25887);
		AIR_SmokeEvacHandle_Pulled = new Offset<byte>(groupName, 25888);
		AIR_Pack_Selector = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 25889),
			new Offset<byte>(groupName, 25890),
			new Offset<byte>(groupName, 25891)
		};
		AIR_TrimAir_Sw_On = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25892),
			new Offset<byte>(groupName, 25893)
		};
		AIR_RecircFan_Sw_On = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25894),
			new Offset<byte>(groupName, 25895)
		};
		AIR_TempSelector = new Offset<byte>[6]
		{
			new Offset<byte>(groupName, 25896),
			new Offset<byte>(groupName, 25897),
			new Offset<byte>(groupName, 25898),
			new Offset<byte>(groupName, 25899),
			new Offset<byte>(groupName, 25900),
			new Offset<byte>(groupName, 25901)
		};
		AIR_PackReset_Sw_Pushed = new Offset<byte>(groupName, 25902);
		AIR_EquipCooling_Selector = new Offset<byte>(groupName, 25903);
		AIR_HighFlow_Sw_On = new Offset<byte>(groupName, 25904);
		AIR_Gasper_Sw_On = new Offset<byte>(groupName, 25905);
		AIR_FltDeckFan_Sw_On = new Offset<byte>(groupName, 25906);
		AIR_AftCargoHeat_Sw_On = new Offset<byte>(groupName, 25907);
		AIR_ZoneReset_Sw_Pushed = new Offset<byte>(groupName, 25908);
		AIR_AltnVent_Sw_On = new Offset<byte>(groupName, 25909);
		AIR_AltnVent_Selector = new Offset<byte>(groupName, 25910);
		AIR_annunPackOFF = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 25911),
			new Offset<byte>(groupName, 25912),
			new Offset<byte>(groupName, 25913)
		};
		AIR_annunPack_SYS_FAIL = new Offset<byte>(groupName, 25914);
		AIR_annunZone_SYS_FAIL = new Offset<byte>(groupName, 25915);
		AIR_annunAftCragoHeat_TEMP = new Offset<byte>(groupName, 25916);
		AIR_EngBleedAir_Sw_ON = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25917),
			new Offset<byte>(groupName, 25918),
			new Offset<byte>(groupName, 25919),
			new Offset<byte>(groupName, 25920)
		};
		AIR_APUBleedAir_Sw_ON = new Offset<byte>(groupName, 25921);
		AIR_IsolationValve_Sw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25922),
			new Offset<byte>(groupName, 25923)
		};
		AIR_annunEngBleedAirOFF = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25924),
			new Offset<byte>(groupName, 25925),
			new Offset<byte>(groupName, 25926),
			new Offset<byte>(groupName, 25927)
		};
		AIR_annunAPUBleedAirVALVE = new Offset<byte>(groupName, 25928);
		AIR_annunIsolationVALVE = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25929),
			new Offset<byte>(groupName, 25930)
		};
		AIR_annun_SYS_FAULT = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25931),
			new Offset<byte>(groupName, 25932),
			new Offset<byte>(groupName, 25933),
			new Offset<byte>(groupName, 25934)
		};
		LTS_EmerLightsSelector = new Offset<byte>(groupName, 25935);
		COMM_CAPTAudio_Selector = new Offset<byte>(groupName, 25936);
		COMM_OBSAudio_Selector = new Offset<byte>(groupName, 25937);
		COMM_ServiceInterphoneSw = new Offset<byte>(groupName, 25938);
		COMM_CargoCabinInterphoneSw = new Offset<byte>(groupName, 25939);
		OXY_Oxygen_Sw_On = new Offset<byte>(groupName, 25940);
		FCTL_YawDamper_Sw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25941),
			new Offset<byte>(groupName, 25942)
		};
		FCTL_annunYawDamperINOP = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25943),
			new Offset<byte>(groupName, 25944)
		};
		WARN_Reset_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25945),
			new Offset<byte>(groupName, 25946)
		};
		WARN_annunMASTER_WARNING = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25947),
			new Offset<byte>(groupName, 25948)
		};
		WARN_annunMASTER_CAUTION = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25949),
			new Offset<byte>(groupName, 25950)
		};
		EFIS_MinsSelBARO = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25951),
			new Offset<byte>(groupName, 25952)
		};
		EFIS_BaroSelHPA = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25953),
			new Offset<byte>(groupName, 25954)
		};
		EFIS_VORADFSel1 = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25955),
			new Offset<byte>(groupName, 25956)
		};
		EFIS_VORADFSel2 = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25957),
			new Offset<byte>(groupName, 25958)
		};
		EFIS_ModeSel = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25959),
			new Offset<byte>(groupName, 25960)
		};
		EFIS_RangeSel = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25961),
			new Offset<byte>(groupName, 25962)
		};
		EFIS_MinsKnob = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25963),
			new Offset<byte>(groupName, 25964)
		};
		EFIS_BaroKnob = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25965),
			new Offset<byte>(groupName, 25966)
		};
		EFIS_MinsRST_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25967),
			new Offset<byte>(groupName, 25968)
		};
		EFIS_BaroSTD_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25969),
			new Offset<byte>(groupName, 25970)
		};
		EFIS_ModeCTR_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25971),
			new Offset<byte>(groupName, 25972)
		};
		EFIS_RangeTFC_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25973),
			new Offset<byte>(groupName, 25974)
		};
		EFIS_WXR_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25975),
			new Offset<byte>(groupName, 25976)
		};
		EFIS_STA_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25977),
			new Offset<byte>(groupName, 25978)
		};
		EFIS_WPT_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25979),
			new Offset<byte>(groupName, 25980)
		};
		EFIS_ARPT_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25981),
			new Offset<byte>(groupName, 25982)
		};
		EFIS_DATA_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25983),
			new Offset<byte>(groupName, 25984)
		};
		EFIS_POS_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25985),
			new Offset<byte>(groupName, 25986)
		};
		EFIS_TERR_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25987),
			new Offset<byte>(groupName, 25988)
		};
		DSP_L_INBD_Sw = new Offset<byte>(groupName, 25989);
		DSP_R_INBD_Sw = new Offset<byte>(groupName, 25990);
		DSP_LWR_CTR_Sw = new Offset<byte>(groupName, 25991);
		DSP_ENG_Sw = new Offset<byte>(groupName, 25992);
		DSP_STAT_Sw = new Offset<byte>(groupName, 25993);
		DSP_ELEC_Sw = new Offset<byte>(groupName, 25994);
		DSP_HYD_Sw = new Offset<byte>(groupName, 25995);
		DSP_FUEL_Sw = new Offset<byte>(groupName, 25996);
		DSP_ECS_Sw = new Offset<byte>(groupName, 25997);
		DSP_DRS_Sw = new Offset<byte>(groupName, 25998);
		DSP_GEAR_Sw = new Offset<byte>(groupName, 25999);
		DSP_FCTL_Sw = new Offset<byte>(groupName, 26000);
		DSP_INFO_Sw = new Offset<byte>(groupName, 26001);
		DSP_CHKL_Sw = new Offset<byte>(groupName, 26002);
		DSP_NAV_Sw = new Offset<byte>(groupName, 26003);
		DSP_CANC_Sw = new Offset<byte>(groupName, 26004);
		DSP_RCL_Sw = new Offset<byte>(groupName, 26005);
		DSP_annunL_INBD = new Offset<byte>(groupName, 26006);
		DSP_annunR_INBD = new Offset<byte>(groupName, 26007);
		DSP_annunLWR_CTR = new Offset<byte>(groupName, 26008);
		MCP_IASMach = new Offset<float>(groupName, 26009);
		MCP_IASBlank = new Offset<byte>(groupName, 26013);
		MCP_Heading = new Offset<ushort>(groupName, 26014);
		MCP_Altitude = new Offset<ushort>(groupName, 26016);
		MCP_VertSpeed = new Offset<short>(groupName, 26018);
		MCP_VertSpeedBlank = new Offset<byte>(groupName, 26020);
		MCP_FD_Sw_On = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26021),
			new Offset<byte>(groupName, 26022)
		};
		MCP_ATArm_Sw_On = new Offset<byte>(groupName, 26023);
		MCP_BankLimitSel = new Offset<byte>(groupName, 26024);
		MCP_DisengageBar = new Offset<byte>(groupName, 26025);
		MCP_Speed_Dial = new Offset<byte>(groupName, 26026);
		MCP_Heading_Dial = new Offset<byte>(groupName, 26027);
		MCP_Altitude_Dial = new Offset<byte>(groupName, 26028);
		MCP_VS_Wheel = new Offset<byte>(groupName, 26029);
		MCP_AP_Sw_Pushed = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 26030),
			new Offset<byte>(groupName, 26031),
			new Offset<byte>(groupName, 26032)
		};
		MCP_THR_Sw_Pushed = new Offset<byte>(groupName, 26033);
		MCP_SPD_Sw_Pushed = new Offset<byte>(groupName, 26034);
		MCP_LNAV_Sw_Pushed = new Offset<byte>(groupName, 26035);
		MCP_VNAV_Sw_Pushed = new Offset<byte>(groupName, 26036);
		MCP_FLCH_Sw_Pushed = new Offset<byte>(groupName, 26037);
		MCP_HDG_HOLD_Sw_Pushed = new Offset<byte>(groupName, 26038);
		MCP_VS_Sw_Pushed = new Offset<byte>(groupName, 26039);
		MCP_ALT_HOLD_Sw_Pushed = new Offset<byte>(groupName, 26040);
		MCP_LOC_Sw_Pushed = new Offset<byte>(groupName, 26041);
		MCP_APP_Sw_Pushed = new Offset<byte>(groupName, 26042);
		MCP_Speeed_Sw_Pushed = new Offset<byte>(groupName, 26043);
		MCP_Heading_Sw_Pushed = new Offset<byte>(groupName, 26044);
		MCP_Altitude_Sw_Pushed = new Offset<byte>(groupName, 26045);
		MCP_IAS_MACH_Toggle_Sw_Pushed = new Offset<byte>(groupName, 26046);
		MCP_annunAP = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 26047),
			new Offset<byte>(groupName, 26048),
			new Offset<byte>(groupName, 26049)
		};
		MCP_annunTHR = new Offset<byte>(groupName, 26050);
		MCP_annunSPD = new Offset<byte>(groupName, 26051);
		MCP_annunLNAV = new Offset<byte>(groupName, 26052);
		MCP_annunVNAV = new Offset<byte>(groupName, 26053);
		MCP_annunFLCH = new Offset<byte>(groupName, 26054);
		MCP_annunHDG_HOLD = new Offset<byte>(groupName, 26055);
		MCP_annunVS = new Offset<byte>(groupName, 26056);
		MCP_annunALT_HOLD = new Offset<byte>(groupName, 26057);
		MCP_annunLOC = new Offset<byte>(groupName, 26058);
		MCP_annunAPP = new Offset<byte>(groupName, 26059);
		DSP_InbdDspl_L_Selector = new Offset<byte>(groupName, 26060);
		DSP_LwrDspl_L_Selector = new Offset<byte>(groupName, 26061);
		DSP_InbdDspl_R_Selector = new Offset<byte>(groupName, 26062);
		DSP_LwrDspl_R_Selector = new Offset<byte>(groupName, 26063);
		ISP_FMC_Selector = new Offset<byte>(groupName, 26064);
		ISP_EIU_C_Selector = new Offset<byte>(groupName, 26065);
		LTS_UpperDsplBRIGHTNESSKnob = new Offset<byte>(groupName, 26066);
		LTS_LowerDsplBRIGHTNESSKnob = new Offset<byte>(groupName, 26067);
		EICAS_EventRcd_Sw_Pushed = new Offset<byte>(groupName, 26068);
		EFIS_HdgRef_Sw_Norm = new Offset<byte>(groupName, 26069);
		FCTL_AltnFlaps_Sw_ARM = new Offset<byte>(groupName, 26070);
		FCTL_AltnFlaps_Control_Sw = new Offset<byte>(groupName, 26071);
		GEAR_Lever = new Offset<byte>(groupName, 26072);
		GEAR_LockOvrd_Sw = new Offset<byte>(groupName, 26073);
		GEAR_AltnGearNoseBody_Sw_DPushed = new Offset<byte>(groupName, 26074);
		GEAR_AltnGearWing_Sw_DPushed = new Offset<byte>(groupName, 26075);
		GPWS_GSInhibit_Sw = new Offset<byte>(groupName, 26076);
		GPWS_annunGND_PROX_top = new Offset<byte>(groupName, 26077);
		GPWS_annunGND_PROX_bottom = new Offset<byte>(groupName, 26078);
		GPWS_FlapInhibitSw_OVRD = new Offset<byte>(groupName, 26079);
		GPWS_GearInhibitSw_OVRD = new Offset<byte>(groupName, 26080);
		GPWS_TerrInhibitSw_OVRD = new Offset<byte>(groupName, 26081);
		ISFD_Baro_Sw_Pushed = new Offset<byte>(groupName, 26082);
		ISFD_RST_Sw_Pushed = new Offset<byte>(groupName, 26083);
		ISFD_Minus_Sw_Pushed = new Offset<byte>(groupName, 26084);
		ISFD_Plus_Sw_Pushed = new Offset<byte>(groupName, 26085);
		ISFD_APP_Sw_Pushed = new Offset<byte>(groupName, 26086);
		ISFD_HP_IN_Sw_Pushed = new Offset<byte>(groupName, 26087);
		ISP_FltDir_L_Selector = new Offset<byte>(groupName, 26088);
		ISP_Nav_L_Selector = new Offset<byte>(groupName, 26089);
		ISP_EIU_L_Selector = new Offset<byte>(groupName, 26090);
		ISP_IRS_L_Selector = new Offset<byte>(groupName, 26091);
		ISP_AirData_L_Selector = new Offset<byte>(groupName, 26092);
		BRAKES_BrakePressNeedle = new Offset<int>(groupName, 26093);
		BRAKES_annunBRAKE_SOURCE = new Offset<byte>(groupName, 26097);
		ISP_FltDir_R_Selector = new Offset<byte>(groupName, 26098);
		ISP_Nav_R_Selector = new Offset<byte>(groupName, 26099);
		ISP_EIU_R_Selector = new Offset<byte>(groupName, 26100);
		ISP_IRS_R_Selector = new Offset<byte>(groupName, 26101);
		ISP_AirData_R_Selector = new Offset<byte>(groupName, 26102);
		LTS_LeftFwdPanelPNLKnob = new Offset<byte>(groupName, 26103);
		LTS_LeftFwdPanelFLOODKnob = new Offset<byte>(groupName, 26104);
		LTS_LeftOutbdDsplBRIGHTNESSKnob = new Offset<byte>(groupName, 26105);
		LTS_LeftInbdDsplBRIGHTNESSKnob = new Offset<byte>(groupName, 26106);
		LTS_RightFwdPanelPNLKnob = new Offset<byte>(groupName, 26107);
		LTS_RightFwdPanelFLOODKnob = new Offset<byte>(groupName, 26108);
		LTS_RightInbdDsplBRIGHTNESSKnob = new Offset<byte>(groupName, 26109);
		LTS_RightOutbdDsplBRIGHTNESSKnob = new Offset<byte>(groupName, 26110);
		AIR_ShoulderHeaterKnob = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26111),
			new Offset<byte>(groupName, 26112)
		};
		AIR_FootHeaterSelector = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25857),
			new Offset<byte>(groupName, 25858)
		};
		AIR_WShldAirSelector = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26115),
			new Offset<byte>(groupName, 26116)
		};
		CHR_Chr_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26117),
			new Offset<byte>(groupName, 26118)
		};
		CHR_Date_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26119),
			new Offset<byte>(groupName, 26120)
		};
		CHR_Set_Selector = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26121),
			new Offset<byte>(groupName, 26122)
		};
		CHR_ET_Selector = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26123),
			new Offset<byte>(groupName, 26124)
		};
		FCTL_StabCutOutSw_2_NORMAL = new Offset<byte>(groupName, 26125);
		FCTL_StabCutOutSw_3_NORMAL = new Offset<byte>(groupName, 26126);
		FCTL_AltnPitch_Switches = new Offset<byte>(groupName, 26127);
		FCTL_Speedbrake_Lever = new Offset<byte>(groupName, 26128);
		FCTL_Flaps_Lever = new Offset<byte>(groupName, 26129);
		ENG_FuelControl_Sw_RUN = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 26130),
			new Offset<byte>(groupName, 26131),
			new Offset<byte>(groupName, 26132),
			new Offset<byte>(groupName, 26133)
		};
		BRAKES_ParkingBrakeLeverOn = new Offset<byte>(groupName, 26134);
		CDU_annunEXEC = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 27648),
			new Offset<byte>(groupName, 27649),
			new Offset<byte>(groupName, 27650)
		};
		CDU_annunDSPY = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 27651),
			new Offset<byte>(groupName, 27652),
			new Offset<byte>(groupName, 27653)
		};
		CDU_annunFAIL = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 27654),
			new Offset<byte>(groupName, 27655),
			new Offset<byte>(groupName, 27656)
		};
		CDU_annunMSG = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 27657),
			new Offset<byte>(groupName, 27658),
			new Offset<byte>(groupName, 27659)
		};
		CDU_annunOFST = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 27660),
			new Offset<byte>(groupName, 27661),
			new Offset<byte>(groupName, 27662)
		};
		CDU_BrtKnob = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 27663),
			new Offset<byte>(groupName, 27664),
			new Offset<byte>(groupName, 27665)
		};
		COMM_SelectedMic = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 27666),
			new Offset<byte>(groupName, 27667),
			new Offset<byte>(groupName, 27668)
		};
		COMM_ReceiverSwitches = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 27669),
			new Offset<byte>(groupName, 27670),
			new Offset<byte>(groupName, 27671)
		};
		COMM_SelectedRadio = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 27672),
			new Offset<byte>(groupName, 27673),
			new Offset<byte>(groupName, 27674)
		};
		COMM_RadioTransfer_Sw_Pushed = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 27675),
			new Offset<byte>(groupName, 27676),
			new Offset<byte>(groupName, 27677)
		};
		COMM_RadioPanelOff = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 27678),
			new Offset<byte>(groupName, 27679),
			new Offset<byte>(groupName, 27680)
		};
		COMM_annunAM = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 27681),
			new Offset<byte>(groupName, 27682),
			new Offset<byte>(groupName, 27683)
		};
		XPDR_XpndrSelector_R = new Offset<byte>(groupName, 27684);
		XPDR_ModeSel = new Offset<byte>(groupName, 27685);
		XPDR_Ident_Sw_Pushed = new Offset<byte>(groupName, 27686);
		BRAKES_AutobrakeSelector = new Offset<byte>(groupName, 27687);
		FCTL_AileronTrim_Switches = new Offset<byte>(groupName, 27688);
		FCTL_RudderTrim_Knob = new Offset<byte>(groupName, 27689);
		SIGNS_NoSmokingSelector = new Offset<byte>(groupName, 27690);
		SIGNS_SeatBeltsSelector = new Offset<byte>(groupName, 27691);
		EVAC_Command_Sw_ON = new Offset<byte>(groupName, 27692);
		EVAC_PressToTest_Sw_Pressed = new Offset<byte>(groupName, 27693);
		EVAC_HornSutOff_Sw_Pulled = new Offset<byte>(groupName, 27694);
		EVAC_LightIlluminated = new Offset<byte>(groupName, 27695);
		DOOR_state = new Offset<byte>[20]
		{
			new Offset<byte>(groupName, 27696),
			new Offset<byte>(groupName, 27697),
			new Offset<byte>(groupName, 27698),
			new Offset<byte>(groupName, 27699),
			new Offset<byte>(groupName, 27700),
			new Offset<byte>(groupName, 27701),
			new Offset<byte>(groupName, 27702),
			new Offset<byte>(groupName, 27703),
			new Offset<byte>(groupName, 27704),
			new Offset<byte>(groupName, 27705),
			new Offset<byte>(groupName, 27706),
			new Offset<byte>(groupName, 27707),
			new Offset<byte>(groupName, 27708),
			new Offset<byte>(groupName, 27709),
			new Offset<byte>(groupName, 27710),
			new Offset<byte>(groupName, 27711),
			new Offset<byte>(groupName, 27712),
			new Offset<byte>(groupName, 27713),
			new Offset<byte>(groupName, 27714),
			new Offset<byte>(groupName, 27715)
		};
		ENG_StartValve = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 27716),
			new Offset<byte>(groupName, 27717),
			new Offset<byte>(groupName, 27718),
			new Offset<byte>(groupName, 27719)
		};
		AIR_DuctPress = new Offset<float>[3]
		{
			new Offset<float>(groupName, 27720),
			new Offset<float>(groupName, 27724),
			new Offset<float>(groupName, 27728)
		};
		FUEL_TankQty = new Offset<float>[9]
		{
			new Offset<float>(groupName, 27732),
			new Offset<float>(groupName, 27736),
			new Offset<float>(groupName, 27740),
			new Offset<float>(groupName, 27744),
			new Offset<float>(groupName, 27748),
			new Offset<float>(groupName, 27752),
			new Offset<float>(groupName, 27756),
			new Offset<float>(groupName, 27760),
			new Offset<float>(groupName, 27764)
		};
		IRS_aligned = new Offset<byte>(groupName, 27768);
		AircraftModel = new Offset<byte>(groupName, 27769);
		WeightInKg = new Offset<byte>(groupName, 27770);
		GPWS_V1CallEnabled = new Offset<byte>(groupName, 27771);
		GroundConnAvailable = new Offset<byte>(groupName, 27772);
		FMC_TakeoffFlaps = new Offset<byte>(groupName, 27773);
		FMC_V1 = new Offset<byte>(groupName, 27774);
		FMC_VR = new Offset<byte>(groupName, 27775);
		FMC_V2 = new Offset<byte>(groupName, 27776);
		FMC_LandingFlaps = new Offset<byte>(groupName, 27777);
		FMC_LandingVREF = new Offset<byte>(groupName, 27778);
		FMC_CruiseAlt = new Offset<ushort>(groupName, 27779);
		FMC_LandingAltitude = new Offset<short>(groupName, 27781);
		FMC_TransitionAlt = new Offset<ushort>(groupName, 27783);
		FMC_TransitionLevel = new Offset<ushort>(groupName, 27785);
		FMC_PerfInputComplete = new Offset<byte>(groupName, 27787);
		FMC_DistanceToTOD = new Offset<float>(groupName, 27788);
		FMC_DistanceToDest = new Offset<float>(groupName, 27792);
		FMC_flightNumber = new Offset<string>(groupName, 27796, 9);
		ELEC_annunBatteryOFF = new Offset<byte>(groupName, 27805);
		FIRE_annunCargoDEPRESS = new Offset<byte>(groupName, 27806);
		MCP_panelPowered = new Offset<byte>(groupName, 27807);
		COMM_RadioPanelPowered = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 27808),
			new Offset<byte>(groupName, 27809),
			new Offset<byte>(groupName, 27810)
		};
		COMM_AudioControlPanelPowered = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 27811),
			new Offset<byte>(groupName, 27812),
			new Offset<byte>(groupName, 27813)
		};
		TCAS_ATC_panelPowered = new Offset<byte>(groupName, 27814);
		FIRE_HandleIllumination = new Offset<byte>[5]
		{
			new Offset<byte>(groupName, 27815),
			new Offset<byte>(groupName, 27816),
			new Offset<byte>(groupName, 27817),
			new Offset<byte>(groupName, 27818),
			new Offset<byte>(groupName, 27819)
		};
		WheelChocksSet = new Offset<byte>(groupName, 27820);
	}

	~PMDG_747QOTSII_Offsets()
	{
		FSUIPCConnection.DeleteGroup(groupName);
	}

	public void RefreshData()
	{
		RefreshData(0);
	}

	public void RefreshData(byte ClassInstance)
	{
		lock (lockObject)
		{
			if (FSUIPCConnection.IsConnectionOpenForClass(ClassInstance))
			{
				FSUIPCConnection.Process(ClassInstance, groupName);
				return;
			}
			if (ClassInstance == 0)
			{
				throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "The connection to FSUIPC is not open.");
			}
			throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "The connection to class instance " + ClassInstance.ToString("D2") + " of WideClient.exe is not open.");
		}
	}
}
