<script setup>
import { onMounted, ref } from "vue";
import FormContainer from "@/components/common/FormContainer.vue";

const subcategories = ref([]);
const attributes = ref([]);
const attributeValues = ref([]);
const promoCodes = ref([]);
const name = ref("");
const slug = ref("");
const title = ref("");
const summary = ref("");
const description = ref("");
const price = ref(0);
const onSale = ref(false);
const promoCodeId = ref();
const unitsInStock = ref(0);
const image = ref(null);
const subcategoryId = ref(null);
const attributeId = ref(null);
const attributeValueId = ref(null);
const productAttributes = ref([]);
const loading = ref(false);

onMounted(() => {
  subcategories.value = [
    { id: 0, name: "Desktop" },
    { id: 1, name: "Notebook" },
    { id: 2, name: "Ultrabook" },
  ];
  promoCodes.value = [
    { id: 0, code: "TP30" },
    { id: 1, code: "TP50" },
  ];
  attributes.value = [
    { id: 0, name: "RAM" },
    { id: 1, name: "SSD" },
    { id: 2, name: "SSD" },
  ];
  attributeValues.value = [
    { id: 0, value: "5 GB" },
    { id: 1, value: "Apple" },
  ];
});

function handleSave() {
  const formData = new FormData();
  formData.append("name", name.value);
  formData.append("slug", slug.value);
  formData.append("title", title.value);
  formData.append("summary", summary.value);
  formData.append("description", description.value);
  formData.append("price", price.value);
  formData.append("onSale", onSale.value);
  formData.append("promoCodeId", promoCodeId.value);
  formData.append("unitsInStock", unitsInStock.value);
  formData.append("image", image.value);
  formData.append("subcategoryId", subcategoryId.value);
  formData.append("productAttributes", JSON.stringify(productAttributes.value));

  console.log(formData);
}

function removeAttribute(attributeId, attributeValueId) {
  productAttributes.value = productAttributes.value.filter(
    (pa) => pa.attribute.id !== attributeId || pa.attributeValue.id !== attributeValueId
  );
}

function handleAttributeValuePairAdd() {
  if (attributeId.value !== null && attributeValueId.value !== null) {
    const pairExists = productAttributes.value.find(
      (av) =>
        av.attribute.id === attributeId.value && av.attributeValue.id === attributeValueId.value
    );

    if (!pairExists) {
      productAttributes.value.push({
        attribute: attributes.value.find((a) => a.id === attributeId.value),
        attributeValue: attributeValues.value.find((av) => av.id === attributeValueId.value),
      });

      attributeId.value = null;
      attributeValueId.value = null;
    }
  }
}
</script>

<template>
  <form-container title="New Product">
    <v-form @submit.prevent>
      <v-text-field
        v-model="name"
        class="mt-5"
        label="Name"
        density="compact"
        variant="outlined"
        hide-details="auto"
      />
      <v-text-field
        v-model="slug"
        class="mt-5"
        label="Slug"
        density="compact"
        variant="outlined"
        hide-details="auto"
      />
      <v-text-field
        v-model="title"
        class="mt-5"
        label="Title"
        density="compact"
        variant="outlined"
        hide-details="auto"
      />
      <v-text-field
        v-model="summary"
        class="mt-5"
        label="Summary"
        density="compact"
        variant="outlined"
        hide-details="auto"
      />
      <v-textarea
        v-model="description"
        class="mt-5"
        label="Description"
        variant="outlined"
        hide-details="auto"
      />
      <v-text-field
        v-model="price"
        type="number"
        class="mt-5"
        label="Price"
        density="compact"
        variant="outlined"
        hide-details="auto"
        min="0"
      />
      <v-checkbox v-model="onSale" class="mt-5" label="On Sale" hide-details="auto"></v-checkbox>
      <v-select
        v-model="promoCodeId"
        class="mt-5"
        item-value="id"
        item-title="code"
        label="Promo Code"
        :items="promoCodes"
        density="compact"
        hide-details="auto"
        variant="outlined"
      ></v-select>
      <v-text-field
        v-model="unitsInStock"
        type="number"
        class="mt-5"
        label="Units in stock"
        density="compact"
        variant="outlined"
        hide-details="auto"
        min="0"
      />
      <v-file-input
        v-model="image"
        class="mt-5"
        label="Image"
        density="compact"
        variant="outlined"
        hide-details="auto"
      ></v-file-input>
      <v-select
        v-model="subcategoryId"
        class="mt-5"
        label="Subcategory"
        :items="subcategories"
        item-value="id"
        item-title="name"
        density="compact"
        hide-details="auto"
        variant="outlined"
      ></v-select>
      <div class="attribue-container">
        <div class="attribute__add">
          <v-select
            v-model="attributeId"
            class="mt-5"
            label="Attribute"
            :items="attributes"
            item-value="id"
            item-title="name"
            density="compact"
            hide-details="auto"
            variant="outlined"
          ></v-select>
          <v-select
            v-model="attributeValueId"
            label="Attribute Value"
            class="mt-sm-5"
            :items="attributeValues"
            item-value="id"
            item-title="value"
            density="compact"
            hide-details="auto"
            variant="outlined"
          ></v-select>
          <v-btn class="btn--add" @click="handleAttributeValuePairAdd">Add</v-btn>
        </div>
        <div class="attribute__list">
          <v-chip v-for="(attributeValuePair, i) in productAttributes" :key="i" class="chip" label>
            {{ attributeValuePair.attribute.name }} {{ attributeValuePair.attributeValue.value }}
            <v-btn
              class="chip__btn"
              icon="mdi-close-circle-outline"
              density="compact"
              @click="
                removeAttribute(
                  attributeValuePair.attribute.id,
                  attributeValuePair.attributeValue.id
                )
              "
            ></v-btn>
          </v-chip>
        </div>
      </div>
      <v-btn type="submit" class="form__btn" :loading="loading" @click="handleSave">Save</v-btn>
    </v-form>
  </form-container>
</template>

<style scoped>
.form__btn {
  margin-top: 30px;
  font-weight: bold;
  background: var(--ts-c-bg-light);
  color: var(--ts-c-text-dark);
  width: 100%;
  max-width: 300px !important;
}

.attribute__add {
  display: flex;
  flex-direction: column;
  justify-content: center;
  gap: 10px;
}

.attribute__list {
  display: flex;
  flex-wrap: wrap;
  align-items: center;
  min-height: 50px;
  margin-top: 15px;
  border: 2px dashed var(--ts-c-bg-light);
  border-radius: 5px;
}

.btn--add {
  margin-top: 20px;
  font-weight: bold;
  background-color: var(--ts-c-bg-light);
  color: var(--ts-c-text-dark);
}

.chip {
  display: flex;
  margin: 5px;
}

.chip__btn {
  margin-left: 10px;
  background: transparent;
  color: var(--ts-c-danger);
  border: none;
  outline: none;
  box-shadow: none !important;
}

@media only screen and (min-width: 40em) {
  .attribute__add {
    flex-direction: row;
    align-items: center;
  }
}
</style>
