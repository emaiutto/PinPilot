

function Brakes()
	_BRAKES()
end

function Brakes_PARKING()
	_PARKING_BRAKES()
end

function NGX_WARN_ALL_RESET()
    NGX_WARN_MASTER_reset()
    NGX_WARN_FIRE_reset()
    NGX_AP_P_reset()
    NGX_AT_P_reset()
    NGX_FMC_P_reset()
end

function NGX_APU_ON_AND_START()
    
	if ipc.readLvar('ngx_switch_118_a') < 50 then
        ipc.control(PMDGBaseVar+118, PMDG_ClkL)
    end
    if ipc.readLvar('ngx_switch_118_a') == 50 then
        ipc.control(PMDGBaseVar+118, PMDG_ClkL)
        --NGX_APU_show()
    end
end

function NGX_STROBE_TOGGLE()
  if ipc.readLvar('ngx_switch_123_a') == 0 then
    NGX_NAV_steady ()
  else
    NGX_NAV_strobe ()
  end
end

function NGX_ANTI_ICE_TOGGLE()
	NGX_ANTI_ICE_WING_toggle()
	NGX_ANTI_ICE_ENG_both_toggle()
end


-- ## AP Rotarys
-- $$ AP CRS (CAPTAIN)

function NGX_AP_CRSL_inc ()
	ipc.control(70008, 16384)
	ipc.sleep(50)
end

function NGX_AP_CRSL_dec ()
	ipc.control(70008, 8192)
	ipc.sleep(50)
end

-- $$ AP CRS (FIRST OFFICER)

function NGX_AP_CRSR_inc ()
	ipc.control(70041, 16384)
	ipc.sleep(50)
end

function NGX_AP_CRSR_dec ()
	ipc.control(70041, 8192)
	ipc.sleep(50)
end

-- $$ AP Speed

function NGX_AP_SPD_inc ()
	ipc.control(70016, 16384)
	ipc.sleep(100)
end

function NGX_AP_SPD_dec ()
    ipc.control(70016, 8192)
	ipc.sleep(100)
end

-- $$ AP Heading

function NGX_AP_HDG_inc ()
	ipc.control(70022, 16384)
	--ipc.sleep(50)
end

function NGX_AP_HDG_dec ()
	ipc.control(70022, 8192)
	--ipc.sleep(50)
end

-- $$ AP Altitude

function NGX_AP_ALT_inc ()
	ipc.control(70032, 16384)
	ipc.sleep(50)
end

function NGX_AP_ALT_dec ()
    ipc.control(70032, 8192)
	ipc.sleep(50)
end

-- $$ AP Vertical Speed

function NGX_AP_VS_inc ()
    ipc.control(70033, 8192)
	ipc.sleep(50)
end

function NGX_AP_VS_dec ()
    ipc.control(70033, 16384)
	ipc.sleep(50)
end

-- ## Engine Switches
-- $$ Engine 1
function NGX_ENG1_idle ()
    if ipc.readLvar('ngx_switch_688_a') ~= 0 then
        ipc.control(PMDGBaseVar+688, PMDG_ClkR)
    end
end

function NGX_ENG1_cutoff ()
    if ipc.readLvar('ngx_switch_688_a') ~= 100 then
        ipc.control(PMDGBaseVar+688, PMDG_ClkL)
    end
end

-- $$ Engine 2
function NGX_ENG2_idle ()
    if ipc.readLvar('ngx_switch_689_a') ~= 0 then
        ipc.control(PMDGBaseVar+689, PMDG_ClkR)
    end
end

function NGX_ENG2_cutoff ()
    if ipc.readLvar('ngx_switch_689_a') ~= 100 then
        ipc.control(PMDGBaseVar+689, PMDG_ClkL)
    end
end
