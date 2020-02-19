﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CompositeBehavior))]
public class CompositeBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CompositeBehavior cb = (CompositeBehavior)target;

        Rect r = EditorGUILayout.BeginHorizontal();

        if(cb.behaviors == null || cb.behaviors.Length == 0){
            //GUILayout.Label("Next line");
            EditorGUILayout.HelpBox("No Behaviors in Array", MessageType.Warning, true);
            EditorGUILayout.EndHorizontal();
        }else{
            EditorGUILayout.LabelField("Number", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
            EditorGUILayout.LabelField("Behaviors", GUILayout.MinWidth(60f));
            EditorGUILayout.LabelField("Weights", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();
            for(int i = 0; i < cb.behaviors.Length; i++){
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(i.ToString(), GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
                cb.behaviors[i] = (FlockBehavior)EditorGUILayout.ObjectField(cb.behaviors[i], typeof(FlockBehavior), false, GUILayout.MinWidth(60f));
                cb.weights[i] = EditorGUILayout.FloatField(cb.weights[i], GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
                EditorGUILayout.EndHorizontal();
            }
            if(EditorGUI.EndChangeCheck()){
                EditorUtility.SetDirty(cb);
            }
        }

        EditorGUILayout.Space(EditorGUIUtility.singleLineHeight*.5f);
        if(GUILayout.Button("Add Behavior")){
                addBehavior(cb);
                EditorUtility.SetDirty(cb);
            }            

            if(cb.behaviors != null && cb.behaviors.Length > 0){
                EditorGUILayout.Space(EditorGUIUtility.singleLineHeight*.5f);
                if(GUILayout.Button("Remove Behavior")){
                    removeBehavior(cb);
                    EditorUtility.SetDirty(cb);
                }
            }
    }

    void removeBehavior(CompositeBehavior cb){
        int oldCount = cb.behaviors.Length;
        if(oldCount == 1){
            cb.behaviors = null;
            cb.weights = null;
            return;
        }

        FlockBehavior[] newBehaviors = new FlockBehavior[oldCount-1];

        float[] newWeights = new float[oldCount-1];

        for(int i = 0; i < oldCount-1; i++){
            newBehaviors[i] = cb.behaviors[i];
            newWeights[i] = cb.weights[i];
        }

        cb.behaviors = newBehaviors;
        cb.weights = newWeights;
    }
    
    void addBehavior(CompositeBehavior cb){
        int oldCount = (cb.behaviors != null) ? cb.behaviors.Length : 0;

        FlockBehavior[] newBehaviors = new FlockBehavior[oldCount+1];

        float[] newWeights = new float[oldCount+1];

        for(int i = 0; i < oldCount; i++){
            newBehaviors[i] = cb.behaviors[i];
            newWeights[i] = cb.weights[i];
        }

        newWeights[oldCount] = 1f;
        cb.behaviors = newBehaviors;
        cb.weights = newWeights;
    }
}
