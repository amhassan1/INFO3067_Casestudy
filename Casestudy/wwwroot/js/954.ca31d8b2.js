"use strict";(globalThis["webpackChunkcasestudyvue"]=globalThis["webpackChunkcasestudyvue"]||[]).push([[954],{5572:(t,e,a)=>{a.d(e,{C:()=>r,_:()=>n});const s="/api/",n=async t=>{let e,a=o();try{let n=await fetch(`${s}${t}`,{method:"GET",headers:a});e=await n.json()}catch(n){console.log(n),e={error:`Error has occured: ${n.message}`}}return e},o=()=>{const t=new Headers,e=JSON.parse(sessionStorage.getItem("customer"));return e?(t.append("Content-Type","application/json"),t.append("Authorization","Bearer "+e.token)):t.append("Content-Type","application/json"),t},r=async(t,e)=>{let a,n=o();try{let o=await fetch(`${s}${t}`,{method:"POST",headers:n,body:JSON.stringify(e)});a=await o.json()}catch(r){a=r}return a}},8954:(t,e,a)=>{a.r(e),a.d(e,{default:()=>$});var s=a(9835),n=a(1957),o=a(6970);const r={class:"text-center"},l=["src"],d=(0,s._)("div",{class:"text-h5 q-mt-lg"},"Find 3 Closest Branches",-1),i=(0,s._)("br",null,null,-1),c={style:{height:"50vh",width:"90vw","margin-left":"5vw","margin-top":"2vh",border:"solid"},ref:"mapRef"};function u(t,e,a,u,p,h){const m=(0,s.up)("q-avatar"),w=(0,s.up)("q-input"),g=(0,s.up)("q-btn");return(0,s.wg)(),(0,s.iD)("div",r,[(0,s.Wm)(m,{class:"q-mt-lg",size:"xl",square:""},{default:(0,s.w5)((()=>[(0,s._)("img",{src:"/img/Running_shoe_icon.png"},null,8,l)])),_:1}),d,(0,s._)("div",null,[(0,s.Wm)(w,{class:"q-ma-lg text-h5",placeholder:"enter current address",id:"address",modelValue:u.state.address,"onUpdate:modelValue":e[0]||(e[0]=t=>u.state.address=t)},null,8,["modelValue"]),i,(0,s.Wm)(g,{label:"3 Closest",onClick:u.genMap,class:"q-mb-md",style:{width:"30vw"}},null,8,["onClick"])]),(0,s.wy)((0,s._)("div",c,null,512),[[n.F8,!0===u.state.showmap]]),(0,s._)("div",null,(0,o.zw)(u.state.status),1)])}var p=a(499),h=a(5572);const m={setup(){const t=(0,p.iH)(null);let e=(0,p.qj)({status:"",address:"",showmap:!1});const a=async()=>{try{e.showmap=!0;const a=window.tt;let s=`https://api.tomtom.com/search/2/geocode/${e.address}.json?key=N1RctBKbaA5QA9ArC4RGUjdlPpCzgBfJ`,n=await fetch(s),o=await n.json(),r=o.results[0].position.lat,l=o.results[0].position.lon,d=await(0,h._)(`Branch/${r}/${l}`),i=a.map({key:"N1RctBKbaA5QA9ArC4RGUjdlPpCzgBfJ",container:t.value,source:"vector/1/basic-main",center:[l,r],zoom:12});i.addControl(new a.FullscreenControl),i.addControl(new a.NavigationControl),d.forEach((t=>{let e=(new a.Marker).setLngLat([t.longitude,t.latitude]).addTo(i),s=25,n=new a.Popup({offset:s});n.setHTML(`<div id="popup">Branch#: ${t.id}</div><div>${t.street}, ${t.city}\n            <br/>${t.distance.toFixed(2)} Km</div>`),e.setPopup(n)}))}catch(a){e.status=a.message}};return{state:e,mapRef:t,genMap:a}}};var w=a(1639),g=a(1357),v=a(6611),y=a(4455),C=a(9984),f=a.n(C);const b=(0,w.Z)(m,[["render",u]]),$=b;f()(m,"components",{QAvatar:g.Z,QInput:v.Z,QBtn:y.Z})}}]);