const products = [
{id:"pliers",name:"Pliers",price:"12.01",description:"General-purpose pliers for gripping, bending, and cutting.",eco:false},
{id:"long-nose-pliers",name:"Long Nose Pliers",price:"14.50",description:"Long-reach pliers for precision work.",eco:true},
{id:"combination-pliers",name:"Combination Pliers",price:"16.25",description:"Versatile combination pliers for workshop tasks.",eco:false},
{id:"claw-hammer",name:"Claw Hammer",price:"18.00",description:"A durable claw hammer for everyday use.",eco:false},
{id:"thor-hammer",name:"Thor Hammer",price:"22.00",description:"A balanced hammer for demanding jobs.",eco:true},
{id:"fiberglass-hammer",name:"Claw Hammer with Fiberglass Handle",price:"20.00",description:"A claw hammer with a shock-absorbing fiberglass handle.",eco:false},
{id:"shock-hammer",name:"Claw Hammer with Shock Reduction Grip",price:"24.00",description:"A hammer designed for a comfortable grip.",eco:true},
{id:"mini-hammer",name:"Mini Hammer",price:"9.00",description:"A compact hammer for light work.",eco:false},
{id:"engineer-hammer",name:"Engineer Hammer",price:"28.00",description:"A sturdy engineer hammer.",eco:false},
{id:"recycled-toolbox",name:"Recycled Tool Box",price:"31.00",description:"A storage box made with recycled materials.",eco:true}
];
function renderProducts(){
 const list=document.querySelector("[data-test='product-list']"); if(!list)return;
 const search=document.querySelector("[data-test='search-query']");
 const ecoFilter=document.querySelector("[data-test='eco-friendly-filter']");
 const term=(search.value||"").trim().toLowerCase();
 const visible=products.filter(p=>p.name.toLowerCase().includes(term)&&(!ecoFilter.checked||p.eco));
 list.innerHTML=visible.map(p=>`<a class="card" data-test="product-${p.id}" href="product.html?id=${p.id}"><h2 data-test="product-name">${p.name}</h2><p>$${p.price}</p>${p.eco?'<span class="badge" data-test="eco-badge">ECO</span>':''}</a>`).join("");
 document.querySelector("[data-test='search-result-count']").textContent=`${visible.length} products found`;
 document.querySelector("#no-products").hidden=visible.length!==0;
}
if(document.body.dataset.page==="home"){
 const search=document.querySelector("[data-test='search-query']");
 search.addEventListener("keydown",event=>{if(event.key==="Enter"){event.preventDefault();renderProducts();search.value="";}});
 document.querySelector("[data-test='eco-friendly-filter']").addEventListener("change",renderProducts);
 const categories=document.querySelector("[data-test='nav-categories']");
 categories.addEventListener("click",()=>{const expanded=categories.getAttribute("aria-expanded")!=="true";categories.setAttribute("aria-expanded",String(expanded));document.querySelector("#categories").hidden=!expanded;});
 renderProducts();
}
if(document.body.dataset.page==="product"){
 const id=new URLSearchParams(location.search).get("id");const product=products.find(p=>p.id===id)||products[0];
 document.title=product.name+" | QA Demo Store";
 document.querySelector("[data-test='product-name']").textContent=product.name;
 document.querySelector("[data-test='unit-price']").textContent=product.price;
 document.querySelector("[data-test='product-description']").textContent=product.description;
}