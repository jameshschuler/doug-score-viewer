<template>
  <div class="field">
    <label class="label">Country</label>
    <div class="tags">
      <div class="tag is-white m-2 p-1 is-clickable tooltip" @click="toggleTag(name)" v-for="{ icon, name, selected } in props.originCountries">
        <span class="tooltiptext">{{ name }}</span>
        <figure class="image is-32x32">
          <img :src="icon" :alt="name" :class="{ 'is-selected': selected }" />
        </figure>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { PropType } from "vue";
import { Country } from "../models/searchQuery";

const props = defineProps({
  originCountries: Array as PropType<Country[]>,
});

function toggleTag(name: string) {
  const tag = props.originCountries!.find((e) => e.name === name);
  tag!.selected = !tag!.selected;
}
</script>
<style lang="scss" scoped>
.tag {
  img {
    opacity: 0.2;
  }

  img.is-selected {
    opacity: 1;
  }
}

/* Tooltip container */
.tooltip {
  position: relative;
  display: inline-block;

  &:hover {
    .tooltiptext {
      visibility: visible;
    }
  }

  .tooltiptext {
    visibility: hidden;
    background-color: hsl(0, 0%, 0%);
    opacity: 0.9;
    color: hsl(0, 0%, 100%);
    text-align: center;
    padding: 5px 0;
    border-radius: 6px;
    width: 120px;
    bottom: 100%;
    left: 50%;
    margin-left: -60px; /* Use half of the width (120/2 = 60), to center the tooltip */
    position: absolute;
    z-index: 1;
  }
}

.tooltip .tooltiptext::after {
  content: " ";
  position: absolute;
  top: 100%; /* At the bottom of the tooltip */
  left: 50%;
  margin-left: -5px;
  border-width: 5px;
  border-style: solid;
  border-color: hsl(0, 0%, 0%) transparent transparent transparent;
}
</style>
