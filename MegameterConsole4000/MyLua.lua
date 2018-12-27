    function unpack (t, i)
      i = i or 1
      if t[i] ~= nil then
        return t[i], unpack(t, i + 1)
      end
    end
getmetatable('').__index = function(str,i) return string.sub(str,i,i) end
getmetatable('').__call = function(str,i,j)  
  if type(i)~='table' then return string.sub(str,i,j) 
    else local t={} 
    for k,v in ipairs(i) do t[k]=string.sub(str,v,v) end
    return table.concat(t)
    end
  end

import("MegameterConsole4000","Database")
import("MegameterConsole4000","LuaLINQ")
import = function () end